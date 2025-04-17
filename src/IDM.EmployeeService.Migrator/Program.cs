using FluentMigrator.Runner;
using IDM.EmployeeService.Migrator.Context;
using IDM.EmployeeService.Migrator.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

var connectionString = configuration.GetSection("DatabaseConnectionOptions:ConnectionString").Get<string>();
var services = new ServiceCollection()
    .AddScoped<IConfiguration>(_ => configuration)
    .AddFluentMigratorCore()
    .ConfigureRunner(
        rb => rb
            .AddPostgres()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(Program).Assembly)
            .For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .AddSingleton<PostgreSqlContext>()
    .AddSingleton<Database>(); ;

var serviceProvider = services.BuildServiceProvider(false);

using (serviceProvider.CreateScope())
{
    var databaseService = serviceProvider.GetRequiredService<Database>();
    await databaseService.InitDatabase("employee-service");

    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    if (args.Contains("--dryrun"))
    {
        runner.ListMigrations();
    }
    else
    {
        runner.MigrateUp();
    }

    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();
    connection.ReloadTypes();
}
