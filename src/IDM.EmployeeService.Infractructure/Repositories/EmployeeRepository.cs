using Dapper;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;
using IDM.EmployeeService.Infractructure.Database.Interfaces;
using IDM.EmployeeService.Infractructure.Repositories.Models;
using Npgsql;
using static IDM.EmployeeService.Infractructure.Database.DatabaseSchemeInfo;

namespace IDM.EmployeeService.Infractructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;

        public EmployeeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<int> CreateAsync(Employee employee, CancellationToken cancellationToken)
        {
            const string sql = @$"
                INSERT INTO {EmployeesTable.TableName}
                (
                    {EmployeesTable.fullname_firstname},
                    {EmployeesTable.fullname_lastname},
                    {EmployeesTable.fullname_middlename},
                    {EmployeesTable.position},
                    {EmployeesTable.email},
                    {EmployeesTable.status_id},
                    {EmployeesTable.hire_date}
                ) 
                VALUES (@FirstName, @LastName, @MiddleName, @Position, @Email, @StatusId, @HireDate) RETURNING id;";

            var parameters = new
            {
                FirstName = employee.FullName.FirstName,
                LastName = employee.FullName.LastName,
                MiddleName = employee.FullName.MiddleName,
                Position = employee.Position.Value,
                Email = employee.Email.Value,
                StatusId = (int)employee.Status,
                HireDate = employee.HireDate,
            };
            var commandDefinition = new CommandDefinition(sql, parameters: parameters, cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.ExecuteScalarAsync(commandDefinition);
            if (result is null)
            {
                throw new InvalidOperationException(nameof(result));
            }

            _changeTracker.Track(employee);

            return (int)result;
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            const string sql = @$"
                SELECT
                    {EmployeesTable.id},
                    {EmployeesTable.fullname_firstname},
                    {EmployeesTable.fullname_lastname},
                    {EmployeesTable.fullname_middlename}
                    {EmployeesTable.position},
                    {EmployeesTable.email},
                    {EmployeesTable.status_id}
                FROM {EmployeesTable.TableName};";

            var commandDefinition = new CommandDefinition(
                sql,
                cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var employees = connection.Query<EmployeeModel>(commandDefinition)
                .Select(employeeModel => new Employee(
                            employeeModel.id,
                            (EmployeeStatus)employeeModel.status_id,
                            employeeModel.creation_date,
                            employeeModel.dismissal_date,
                            FullName.Create(employeeModel.fullname_firstname, employeeModel.fullname_lastname, employeeModel.fullname_middlename),
                            Position.Create(employeeModel.position),
                            Email.Create(employeeModel.email)))
                .ToList();

            return employees;
        }

        public async Task<Employee> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            const string sql = @$"
                SELECT
                    {EmployeesTable.id},
                    {EmployeesTable.fullname_firstname},
                    {EmployeesTable.fullname_lastname},
                    {EmployeesTable.fullname_middlename}
                    {EmployeesTable.position},
                    {EmployeesTable.email},
                    {EmployeesTable.status_id}
                FROM {EmployeesTable.TableName}
                WHERE {EmployeesTable.email} = @email;";

            var parameters = new
            {
                email = email,
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var employees = connection.Query<EmployeeModel>(commandDefinition)
                .Select(employeeModel => new Employee(
                            employeeModel.id,
                            (EmployeeStatus)employeeModel.status_id,
                            employeeModel.creation_date,
                            employeeModel.dismissal_date,
                            FullName.Create(employeeModel.fullname_firstname, employeeModel.fullname_lastname, employeeModel.fullname_middlename),
                            Position.Create(employeeModel.position),
                            Email.Create(employeeModel.email)))
                .ToList();

            return employees.FirstOrDefault();
        }

        public async Task<Employee> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            const string sql = @$"
                SELECT
                    {EmployeesTable.id},
                    {EmployeesTable.fullname_firstname},
                    {EmployeesTable.fullname_lastname},
                    {EmployeesTable.fullname_middlename},
                    {EmployeesTable.position},
                    {EmployeesTable.email},
                    {EmployeesTable.status_id}
                FROM {EmployeesTable.TableName}
                WHERE {EmployeesTable.id} = @id;";

            var parameters = new
            {
                id = id,
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                cancellationToken: cancellationToken);

            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var employees = connection.Query<EmployeeModel>(commandDefinition)
                .Select(employeeModel => new Employee(
                            employeeModel.id,
                            (EmployeeStatus)employeeModel.status_id,
                            employeeModel.creation_date,
                            employeeModel.dismissal_date,
                            FullName.Create(employeeModel.fullname_firstname, employeeModel.fullname_lastname, employeeModel.fullname_middlename),
                            Position.Create(employeeModel.position),
                            Email.Create(employeeModel.email)))
                .ToList();

            return employees.FirstOrDefault();
        }

        public async Task UpdateAsync(Employee employeeToUpdate, CancellationToken cancellationToken)
        {
            const string sql = @$"
                UPDATE {EmployeesTable.TableName}
                SET {EmployeesTable.fullname_firstname} = @FirstName, 
                    {EmployeesTable.fullname_lastname} = @LastName, 
                    {EmployeesTable.fullname_middlename} = @MiddleName, 
                    {EmployeesTable.position} = @Position, 
                    {EmployeesTable.email} = @Email, 
                    {EmployeesTable.status_id} = @StatusId, 
                    {EmployeesTable.hire_date} = @HireDate, 
                    {EmployeesTable.dismissal_date} = @DismissalDate
                WHERE {EmployeesTable.id} = @Id;";

            var parameters = new
            {
                Id = employeeToUpdate.Id,
                FirstName = employeeToUpdate.FullName.FirstName,
                LastName = employeeToUpdate.FullName.LastName,
                MiddleName = employeeToUpdate.FullName.MiddleName,
                Position = employeeToUpdate.Position.Value,
                Email = employeeToUpdate.Email.Value,
                StatusId = (int)employeeToUpdate.Status,
                HireDate = employeeToUpdate.HireDate,
                DismissalDate = employeeToUpdate.DismissalDate
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);

            _changeTracker.Track(employeeToUpdate);
        }
    }
}
