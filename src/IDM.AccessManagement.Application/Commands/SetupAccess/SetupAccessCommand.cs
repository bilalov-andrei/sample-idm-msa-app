using IDM.Common.Application.Commands;
using MediatR;

namespace IDM.AccessManagement.Application.Commands.SetupAccess
{
    public class SetupAccessCommand : CommandBase<Unit>
    {
        /// <summary>
        /// Идентификатор работника
        /// </summary>
        public int EmployeeId { get; }

        /// <summary>
        /// Должность работника
        /// </summary>
        public string EmployeeName { get; }

        /// <summary>
        /// Должность работника
        /// </summary>
        public string EmployeePosition { get; }

        public SetupAccessCommand(int employeeId, string employeeName, string employeePosition)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            EmployeePosition = employeePosition;
        }

    }
}
