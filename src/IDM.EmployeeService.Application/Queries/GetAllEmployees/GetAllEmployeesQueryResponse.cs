namespace IDM.EmployeeService.Application.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryResponse
    {
        /// <summary>
        ///     Founded Employees
        /// </summary>
        public IReadOnlyCollection<EmployeeDto> Items { get; set; }
    }
}
