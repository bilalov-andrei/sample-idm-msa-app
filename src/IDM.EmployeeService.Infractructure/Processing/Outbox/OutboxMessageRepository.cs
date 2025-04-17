using Confluent.Kafka;
using Dapper;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;
using IDM.EmployeeService.Infractructure.Database.Interfaces;
using IDM.EmployeeService.Infractructure.Processing.Outbox;
using IDM.EmployeeService.Infractructure.Repositories.Models;
using Npgsql;
using System.Text;
using static IDM.EmployeeService.Infractructure.Database.DatabaseSchemeInfo;

namespace IDM.EmployeeService.Infractructure.Repositories
{
    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;

        public OutboxMessageRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(OutboxMessage message, CancellationToken cancellationToken)
        {
            const string sql = @$"
                INSERT INTO {OutboxMessagesTable.TableName}
                (
                    {OutboxMessagesTable.createdat},
                    {OutboxMessagesTable.key},
                    {OutboxMessagesTable.eventtype},
                    {OutboxMessagesTable.payload},
                    {OutboxMessagesTable.failed},
                    {OutboxMessagesTable.retrycount},
                    {OutboxMessagesTable.retryafter}
                ) 
                VALUES (@CreatedAt, @Key, @EventType, @Payload, @Failed, @RetryCount, @RetryAfter) RETURNING id;";

            var parameters = new
            {
                CreatedAt = message.CreatedAt,
                Key = message.Key,
                EventType = message.EventType,
                Payload = message.Payload,
                Failed = message.Failed,
                Type = message.EventType,
                RetryCount = message.RetryCount,
                RetryAfter = message.RetryAfter
                
            };
            var commandDefinition = new CommandDefinition(sql, parameters: parameters, cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.ExecuteScalarAsync(commandDefinition);
            if (result is null)
            {
                throw new InvalidOperationException(nameof(result));
            }

            return (int)result;
        }

        //TODO простая реализация паттерна Outbox. Для продакшн нужд
        //использовать https://www.youtube.com/watch?v=b42gkdta_6s и https://www.youtube.com/watch?v=snZSWJ6KVXU
        public async Task<OutboxMessage[]> GetForProcessingAsync(OutboxConfigurationOptions _outboxOptions, CancellationToken cancellationToken)
        {
            const string sql = @$"
                SELECT
                    {OutboxMessagesTable.id},
                    {OutboxMessagesTable.createdat},
                    {OutboxMessagesTable.key},
                    {OutboxMessagesTable.payload},
                    {OutboxMessagesTable.failed},
                    {OutboxMessagesTable.eventtype},
                    {OutboxMessagesTable.retrycount},
                    {OutboxMessagesTable.retryafter},
                    {OutboxMessagesTable.processedat}
                FROM {OutboxMessagesTable.TableName}
                WHERE {OutboxMessagesTable.processedat} IS NULL
                    AND {OutboxMessagesTable.failed} = false
                    AND ({OutboxMessagesTable.retryafter} IS NULL OR {OutboxMessagesTable.retryafter} < @retryafter)
                ORDER BY {OutboxMessagesTable.createdat}
                LIMIT (@batchSize)
                FOR UPDATE SKIP LOCKED;";

            var parameters = new
            {
                retryafter = DateTime.UtcNow,
                batchSize = _outboxOptions.BatchSize

            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters,
                cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var messages = connection.Query<dynamic>(commandDefinition)
                .Select(message => new OutboxMessage()
                {
                    Id = message.id,
                    CreatedAt = message.createdat,
                    Key = message.key,
                    Payload = message.payload,
                    Failed = message.failed,
                    EventType = message.type,
                    RetryCount = message.retrycount,
                    RetryAfter = message.retryafter,
                    ProcessedAt = message.processedat
                })
                .ToArray();

            return messages;
        }

        //TODO use bulk update
        public async Task UpdateAsync(OutboxMessage[] outboxMessages, CancellationToken cancellationToken)
        {
            foreach (var outboxMessage in outboxMessages)
            {
                const string sql = @$"
                UPDATE {OutboxMessagesTable.TableName}
                SET {OutboxMessagesTable.failed} = @failed, 
                    {OutboxMessagesTable.processedat} = @processedat, 
                    {OutboxMessagesTable.retryafter} = @retryafter, 
                    {OutboxMessagesTable.retrycount} = @retrycount
                WHERE {OutboxMessagesTable.id} = @Id;";

                var parameters = new
                {
                    Id = outboxMessage.Id,
                    failed = outboxMessage.Failed,
                    retryafter = outboxMessage.RetryAfter,
                    processedat = outboxMessage.ProcessedAt,
                    retrycount = outboxMessage.RetryCount
                };
                var commandDefinition = new CommandDefinition(
                    sql,
                    parameters: parameters,
                    cancellationToken: cancellationToken);
                var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
                await connection.ExecuteAsync(commandDefinition);
            }
        }
    }
}
