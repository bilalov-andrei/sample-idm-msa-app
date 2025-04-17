using FluentValidation;
using IDM.Common.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IDM.EmployeeService.API.Middlewares
{
    public sealed class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                ProblemDetails problemDetails = null;
                int? statusCode;

                switch (exception)
                {
                    case ValidationException validationException:
                        problemDetails = new ProblemDetails
                        {
                            Type = "ValidationFailure",
                            Title = "Validation error",
                            Detail = "One or more validation errors has occurred"
                        };

                        if (validationException.Errors is not null)
                        {
                            problemDetails.Extensions["errors"] = validationException.Errors;
                        }

                        statusCode = StatusCodes.Status400BadRequest;

                        break;
                    case InvalidOperationException:
                    case CorruptedInvariantException:
                        statusCode = StatusCodes.Status400BadRequest;
                        break;

                    default:
                        statusCode = StatusCodes.Status500InternalServerError;
                        problemDetails = new()
                        {
                            Status = StatusCodes.Status500InternalServerError,
                            Title = "Извините, произошла непредвиденная ошибка",
                            Detail = "Мы уже работаем над ее устранением. Пожалуйста, попробуйте снова позже",
                        };

                        break;
                }

                problemDetails ??= new()
                {
                    Status = statusCode,
                    Title = exception.Message,
                };

                logger.LogError(exception, "Произошло исключение: {Message}", exception.Message);

                context.Response.StatusCode = statusCode.Value;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
