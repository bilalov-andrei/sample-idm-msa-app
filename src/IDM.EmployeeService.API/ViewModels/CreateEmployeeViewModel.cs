using System;
using System.ComponentModel.DataAnnotations;

namespace IDM.EmployeeService.API.InputModels
{
    /// <summary>
    /// Модель для создания нового сотрудника
    /// </summary>
    public sealed class CreateEmployeeViewModel : IEmployeeModel
    {
        /// <inheritdoc cref="FirstName"/>
        [Required]
        public string FirstName { get; set; }

        /// <inheritdoc cref="LastName"/>
        [Required]
        public string LastName { get; set; }

        /// <inheritdoc cref="MiddleName"/>
        [Required]
        public string MiddleName { get; set; }

        /// <inheritdoc cref="Email"/>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <inheritdoc cref="Email"/>
        [Required]
        public string Position { get; set; }

    }
}
