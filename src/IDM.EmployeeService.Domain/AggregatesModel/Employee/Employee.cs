using IDM.Common.Domain;
using IDM.EmployeeService.Domain.Events;

namespace IDM.EmployeeService.Domain.AggregatesModel.Employee
{
    public class Employee : Entity<int>
    {
        /// <summary>
        /// Конструктор для создания объекта из базы данных
        /// </summary>
        public Employee(int id, EmployeeStatus status, DateTime hiredate, DateTime? dismissalDate, FullName fullname, Position position, Email email)
        {
            Id = id;
            Status = status;
            HireDate = hiredate;
            DismissalDate = dismissalDate;
            Email = email;
            FullName = fullname;
            Position = position;
        }

        public Employee(FullName fullname, Position position, Email email)
        {
            Email = email;
            FullName = fullname;
            Position = position;

            Status = EmployeeStatus.Work;
            HireDate = DateTime.UtcNow;

            AddDomainEvent(new EmployeeHiredDomainEvent(this));
        }

        /// <summary>
        /// Адрес электронной почты сотрудника
        /// </summary>
        public Email Email { get; }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public FullName FullName { get; }

        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Статус сотрудника
        /// </summary>
        public EmployeeStatus Status { get; private set; }

        /// <summary>
        /// Дата принятия на работу
        /// </summary>
        public DateTime HireDate { get; }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? DismissalDate { get; private set; }

        /// <summary>
        /// Возвращает true, если сотрудник уволен
        /// </summary>
        public bool IsDismissed()
        {
            return Status == EmployeeStatus.Dismissed;
        }

        public void Dismiss()
        {
            if (Status == EmployeeStatus.Dismissed)
            {
                throw new CorruptedInvariantException($"Employee is already dismissed.");
            }

            Status = EmployeeStatus.Dismissed;
            DismissalDate = DateTime.UtcNow;

            AddDomainEvent(new EmployeeDismissedDomainEvent(this));
        }
    }
}
