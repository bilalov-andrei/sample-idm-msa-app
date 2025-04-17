using IDM.AccessManagement.Domain.UserAccountAggregate;
using IDM.AccessManagement.Infractructure.Configuration;
using IDM.AccessManagement.Infractructure.Database;
using IDM.AccessManagement.Infractructure.Database.Interfaces;
using IDM.AccessManagement.Infractructure.Processing;
using IDM.AccessManagement.Infractructure.Repositories;
using IDM.AccessManagement.Infrastructure.Processing;
using IDM.Common.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace IDM.AccessManagement.Infractructure.Extensions
{
    /// <summary>
    /// Class with extensions for <see cref="IServiceCollection" />
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfractructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection(nameof(DatabaseConnectionOptions)));
            services.Configure<KafkaConfigurationOptions>(configuration.GetSection(nameof(KafkaConfigurationOptions)));

            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
            services.AddScoped<IQueryExecutor, QueryExecutor>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            services.AddScoped<IUserAccountRepository, UserAccountRepository>();

            return services;
        }
    }
}
