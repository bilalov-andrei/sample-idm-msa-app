using IDM.EmployeeService.API.BackgroundServices;
using IDM.EmployeeService.API.Mapper;
using IDM.EmployeeService.Infractructure.Database;
using Microsoft.OpenApi.Models;

namespace IDM.EmployeeService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApiMapperProfile).Assembly);
            services.AddSingleton<ProcessOutboxMessagesBackgroundService>();
            services.AddHostedService(sp => sp.GetRequiredService<ProcessOutboxMessagesBackgroundService>());

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Employees Service",
                    Description = "Service that manages company employees"
                });
            });

            return services;
        }

    }
}
