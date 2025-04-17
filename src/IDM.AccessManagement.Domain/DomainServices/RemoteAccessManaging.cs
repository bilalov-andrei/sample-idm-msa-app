using IDM.AccessManagement.Domain.DomainServices.Models;
using IDM.AccessManagement.Domain.UserAccountAggregate;

namespace IDM.AccessManagement.Domain.DomainServices
{
    public class RemoteAccessManaging : AccessManagingSystem
    {
        public override int SystemId { get; }

        public RemoteAccessManaging()
        {
            SystemId = 1;
        }

        public override async Task SetUpAccess(IUserAccountRepository userAccountRepository, EmployeeWorkInfo employeeWorkInfo, CancellationToken cancellationToken)
        {
            var userAccount = (await userAccountRepository.GetAll(employeeWorkInfo.Id, SystemId, cancellationToken)).FirstOrDefault();

            if (!ShouldHaveAccess(employeeWorkInfo))
            {
                userAccount?.Revoke();
                return;
            }

            if (userAccount is null)
            {
                userAccount = new UserAccount(SystemId, new Employee(employeeWorkInfo.Id, employeeWorkInfo.Name));
                await userAccountRepository.CreateAsync(userAccount, cancellationToken);
            }

            userAccount.Rights.Clear();

            if (employeeWorkInfo.Position.Contains("remote inside country"))
            {
                userAccount.Rights.Add(Right.Create("local vpn access"));
            }

            if (employeeWorkInfo.Position == "remote outside country")
            {
                userAccount.Rights.Add(Right.Create("all vpn access"));
            }

            await userAccountRepository.UpdateAsync(userAccount, cancellationToken);
        }

        private static bool ShouldHaveAccess(EmployeeWorkInfo employeeWorkInfo)
        {
            return employeeWorkInfo.Position.Contains("remote");
        }
    }
}
