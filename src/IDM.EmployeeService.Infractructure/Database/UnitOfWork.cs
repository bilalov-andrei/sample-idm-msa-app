using IDM.Common.Domain;
using IDM.EmployeeService.Infractructure.Database.Exceptions;
using IDM.EmployeeService.Infractructure.Database.Interfaces;
using IDM.EmployeeService.Infrastructure.Processing;
using Npgsql;

namespace IDM.EmployeeService.Infractructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private NpgsqlTransaction _npgsqlTransaction;

        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async ValueTask StartTransaction(CancellationToken token)
        {
            if (_npgsqlTransaction is not null)
            {
                return;
            }
            var connection = await _dbConnectionFactory.CreateConnection(token);
            _npgsqlTransaction = await connection.BeginTransactionAsync(token);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            if (_npgsqlTransaction is null)
            {
                throw new NoActiveTransactionStartedException();
            }

            await _domainEventsDispatcher.DispatchEventsAsync(cancellationToken);
            await _npgsqlTransaction.CommitAsync(cancellationToken);
        }

        void IDisposable.Dispose()
        {
            _npgsqlTransaction?.Dispose();
            _dbConnectionFactory?.Dispose();
        }
    }
}
