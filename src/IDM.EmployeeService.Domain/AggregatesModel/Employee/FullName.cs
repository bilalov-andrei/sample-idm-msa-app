using IDM.Common.Domain;

namespace IDM.EmployeeService.Domain.AggregatesModel.Employee
{
    /// <summary>
    /// ValueObject для описания полного имени сотрудника
    /// </summary>
    public class FullName : ValueObject
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; }

        private FullName(string firstName, string lastName, string middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public static FullName Create(string firstName, string lastName, string middleName)
        {
            return new FullName(firstName, lastName, middleName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return MiddleName;
            yield return LastName;
        }
    }
}
