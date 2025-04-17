using IDM.AccessManagement.Infractructure.Database.Interfaces;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace IDM.AccessManagement.Infractructure.Database
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory<NpgsqlConnection>
    {
        private readonly DatabaseConnectionOptions _options;
        private NpgsqlConnection _connection;
        public NpgsqlConnectionFactory(IOptions<DatabaseConnectionOptions> options)
        {
            _options = options.Value;
        }

        public async Task<NpgsqlConnection> CreateConnection(CancellationToken token)
        {
            if (_connection != null)
            {
                return _connection;
            }

            _connection = new NpgsqlConnection(_options.ConnectionString);
            //_connection = new NpgsqlConnection("Host=host.docker.internal;Port=5424;Database=employee-service;Username=employee_service_admin;Password=employee_service_password");
            //Host=employee_service_db;Port=5432;Database=employee-service;Username=employee_service_admin;Password=employee_service_password
            await _connection.OpenAsync(token);
            _connection.StateChange += (o, e) =>
            {
                if (e.CurrentState == ConnectionState.Closed)
                {
                    _connection = null;
                }
            };
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
