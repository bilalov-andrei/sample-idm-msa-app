namespace IDM.AccessManagement.Domain.UserAccountAggregate
{
    public interface IUserAccountRepository
    {
        Task<int> CreateAsync(UserAccount order, CancellationToken cancellationToken);

        Task<List<UserAccount>> GetAll(int? employeeID, int? systemId, CancellationToken cancellationToken);

        Task UpdateAsync(UserAccount userAccount, CancellationToken cancellationToken);
    }
}
