using IDM.EmployeeService.API.InputModels;

namespace IDM.EmployeeService.API.Employees
{
    public sealed class EmployeeViewModel : IEmployeeModel
    {
        /// <inheritdoc cref="FirstName"/>
        public string FirstName { get; set; }

        /// <inheritdoc cref="LastName"/>
        public string LastName { get; set; }

        /// <inheritdoc cref="MiddleName"/>
        public string MiddleName { get; set; }

        /// <inheritdoc cref="Email"/>
        public string Email { get; set; }

        /// <inheritdoc cref="Email"/>
        public string Position { get; set; }

    }
}
