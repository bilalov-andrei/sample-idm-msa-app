using System.Collections.Generic;

namespace IDM.AccessManagement.Application.Queries.GetUserAccounts
{
    /// <summary>
    /// UserAccount DTO object
    /// </summary>
    public class UserAccountDto
    {
        /// <summary>
        /// SystemID
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// EmployeeID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Права в системе
        /// </summary>
        public List<string> Rights { get; set; }

    }
}
