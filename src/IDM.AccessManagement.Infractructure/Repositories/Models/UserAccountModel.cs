namespace IDM.AccessManagement.Infractructure.Repositories.Models
{
    /// <summary>
    /// database representation of UserAccount aggregate
    /// </summary>
    internal class UserAccountModel
    {
        public int id { get; set; }
        public string system_id { get; set; }
        public string employee_id { get; set; }
        public string status_id { get; set; }
        public string fullname_middlename { get; set; }
        public string created_date { get; set; }
        public int revoked_date { get; set; }
        public string rights { get; set; }
    }
}
