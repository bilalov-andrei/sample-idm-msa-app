using IDM.Common.Domain;

namespace IDM.AccessManagement.Domain.UserAccountAggregate
{
    public class UserAccount : Entity<int>
    {
        /// <summary>
        /// Конструктор для создания объекта из базы данных
        /// </summary>
        public UserAccount(int id, int systemId, Employee employee, UserAccountStatus status, DateTime createdDate, DateTime? deletedDate)
        {
            Id = id;
            Status = status;
            CreatedDate = createdDate;
            RevokedDate = deletedDate;
            SystemId = systemId;
            Employee = employee;
        }

        public UserAccount(int systemId, Employee employee)
        {
            Status = UserAccountStatus.Active;
            CreatedDate = DateTime.UtcNow;
            SystemId = systemId;
            Employee = employee;
        }

        /// <summary>
        /// Идентификатор системы, к которой выдаются права
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public Employee Employee { get; }

        /// <summary>
        /// Выданные аккаунту права
        /// </summary>
        public List<Right> Rights { get; }

        /// <summary>
        /// Статус аккаунта
        /// </summary>
        public UserAccountStatus Status { get; private set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; }

        /// <summary>
        /// Дата отзыва
        /// </summary>
        public DateTime? RevokedDate { get; private set; }

        /// <summary>
        /// Возвращает true, если аккаунт отозван
        /// </summary>
        public bool IsRevoked()
        {
            return Status == UserAccountStatus.Revoked;
        }

        public void Revoke()
        {
            if (Status == UserAccountStatus.Revoked)
            {
                throw new CorruptedInvariantException($"User account is already revoked.");
            }

            Status = UserAccountStatus.Revoked;
            RevokedDate = DateTime.UtcNow;
            Rights.Clear();
        }
    }
}
