
using IDM.EmployeeService.API.Extensions;
using IDM.EmployeeService.API.Middlewares;
using IDM.EmployeeService.Application.Configuration.Extensions;
using IDM.EmployeeService.Infractructure.Extensions;

namespace IDM.EmployeeService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddHostedServices();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCustomSwagger();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfractructure(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
