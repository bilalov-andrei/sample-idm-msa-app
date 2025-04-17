using IDM.AccessManagement.Domain.UserAccountAggregate;
using IDM.AccessManagement.Infractructure.Database.Interfaces;
using Npgsql;

namespace IDM.AccessManagement.Infractructure.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;

        public UserAccountRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public Task<int> CreateAsync(UserAccount order, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserAccount>> GetAll(int? employeeID, int? systemId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserAccount userAccount, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
