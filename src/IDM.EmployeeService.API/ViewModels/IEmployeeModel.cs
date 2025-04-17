namespace IDM.EmployeeService.API.InputModels
{
    public interface IEmployeeModel
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        string MiddleName { get; set; }

        /// <summary>
        /// Email адрес
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        string Position { get; set; }

    }
}
