using IDM.Common.Application.Commands;
using MediatR;

namespace IDM.AccessManagement.Application.Commands.RevokeAccess
{
    public class RevokeAccessCommand : CommandBase<Unit>
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int EmployeeId { get; }

        public RevokeAccessCommand(int id)
        {
            EmployeeId = id;
        }
    }
}
