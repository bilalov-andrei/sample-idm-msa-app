
using CSharpCourse.EmployeesService.ApplicationServices.MessageBroker;
using IDM.Common.Domain;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;
using IDM.EmployeeService.Infractructure.Database;
using IDM.EmployeeService.Infractructure.Database.Interfaces;
using IDM.EmployeeService.Infractructure.MessageBroker;
using IDM.EmployeeService.Infractructure.Processing.Outbox;
using IDM.EmployeeService.Infractructure.Repositories;
using IDM.EmployeeService.Infrastructure.Processing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace IDM.EmployeeService.Infractructure.Extensions
{
    /// <summary>
    /// Class with extensions for <see cref="IServiceCollection" />
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfractructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection(nameof(DatabaseConnectionOptions)));
            services.Configure<OutboxConfigurationOptions>(configuration.GetSection(nameof(OutboxConfigurationOptions)));
            services.Configure<KafkaConfigurationOptions>(configuration.GetSection(nameof(KafkaConfigurationOptions)));

            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
            services.AddScoped<IQueryExecutor, QueryExecutor>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();

            services.AddSingleton<IProducerBuilderWrapper, ProducerBuilderWrapper>();

            return services;
        }
    }
}
