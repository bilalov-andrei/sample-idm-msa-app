using IDM.Common.Application.Commands;

namespace IDM.EmployeeService.Application.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : CommandBase<int>
    {
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Middle name
        /// </summary>
        public string MiddleName { get; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; }

        public CreateEmployeeCommand(string firstName, string lastName, string middleName, string email, string position)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
            Position = position;
        }

    }
}
