using IDM.AccessManagement.Infractructure.Database.Exceptions;
using IDM.AccessManagement.Infractructure.Database.Interfaces;
using IDM.AccessManagement.Infrastructure.Processing;
using IDM.Common.Domain;
using MediatR;
using Npgsql;

namespace IDM.AccessManagement.Infractructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private NpgsqlTransaction _npgsqlTransaction;

        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IPublisher publisher,
            IChangeTracker changeTracker,
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
