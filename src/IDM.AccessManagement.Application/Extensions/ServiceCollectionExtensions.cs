using FluentValidation;
using IDM.AccessManagement.Application.Commands.SetupAccess;
using IDM.AccessManagement.Application.Mapper;
using IDM.Common.Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace IDM.AccessManagement.Application.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining(typeof(SetupAccessCommandHandler));
                config.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
            });

            services.AddAutoMapper(typeof(ApplicationServicesMapperProfile).Assembly);
            services.AddValidatorsFromAssembly(typeof(SetupAccessCommandHandler).Assembly);
            return services;
        }

    }
}
