using IDM.Common.Application.Commands;
using MediatR;

namespace IDM.EmployeeService.Application.Handlers.DismissEmployee
{
    public class DismissEmployeeCommand : CommandBase<Unit>
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int Id { get; }

        public DismissEmployeeCommand(int id)
        {
            Id = id;
        }
    }
}
