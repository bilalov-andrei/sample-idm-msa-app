using FluentValidation;
using IDM.Common.Application.Validation;
using IDM.EmployeeService.Application.Commands.CreateEmployee;
using IDM.EmployeeService.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace IDM.EmployeeService.Application.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining(typeof(CreateEmployeeCommandHandler));
                config.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
            });

            services.AddAutoMapper(typeof(ApplicationServicesMapperProfile).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateEmployeeCommandHandler).Assembly);
            return services;
        }

    }
}
