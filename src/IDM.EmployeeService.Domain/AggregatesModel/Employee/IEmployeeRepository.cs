namespace IDM.EmployeeService.Domain.AggregatesModel.Employee
{
    public interface IEmployeeRepository
    {
        Task<int> CreateAsync(Employee order, CancellationToken cancellationToken);

        Task<Employee> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<Employee> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken);

        Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
    }
}
