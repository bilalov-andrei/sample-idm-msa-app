namespace IDM.EmployeeService.Application.Queries.GetAllEmployees
{
    /// <summary>
    /// Employee of company
    /// </summary>
    public class EmployeeDto
    {
        public int Id { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Middle name
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }

    }
}
