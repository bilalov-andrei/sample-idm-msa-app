using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace IDM.EmployeeService.Migrator.Context
{
    public class PostgreSqlContext
    {
        private readonly IConfiguration _configuration;
        public PostgreSqlContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateMasterConnection()
            => new NpgsqlConnection(_configuration.GetSection("DatabaseConnectionOptions:MasterConnectionString").Get<string>());

    }
}
