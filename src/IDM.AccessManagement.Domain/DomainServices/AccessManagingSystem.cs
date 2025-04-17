using IDM.AccessManagement.Domain.DomainServices.Models;
using IDM.AccessManagement.Domain.UserAccountAggregate;

namespace IDM.AccessManagement.Domain.DomainServices
{
    public abstract class AccessManagingSystem
    {
        public abstract int SystemId { get; }

        public abstract Task SetUpAccess(IUserAccountRepository userAccountRepository, EmployeeWorkInfo employeeWorkInfo, CancellationToken cancellationToken);
    }
}
