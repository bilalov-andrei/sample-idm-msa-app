namespace IDM.EmployeeService.Infractructure.Repositories.Models
{
    /// <summary>
    /// database representation of Employee aggregate
    /// </summary>
    internal class EmployeeModel
    {
        public int id { get; set; }
        public string email { get; set; }
        public string fullname_firstname { get; set; }
        public string fullname_lastname { get; set; }
        public string fullname_middlename { get; set; }
        public string position { get; set; }
        public int status_id { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime? dismissal_date { get; set; }
    }
}
