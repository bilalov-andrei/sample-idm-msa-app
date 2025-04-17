using Dapper;
using IDM.EmployeeService.Migrator.Context;

namespace IDM.EmployeeService.Migrator.Migrations
{
    public class Database
    {
        private readonly PostgreSqlContext _context;
        public Database(PostgreSqlContext context)
        {
            _context = context;
        }

        public async Task InitDatabase(string dbName)
        {
            using (var connection = _context.CreateMasterConnection())
            {
                var isDatabaseExists = await connection.ExecuteScalarAsync<bool>("select count(1) from pg_database where datname = @dbName", new { dbName });
                if (!isDatabaseExists)
                    await connection.ExecuteAsync($"CREATE DATABASE \"{dbName}\"");
            }
        }
    }
}

