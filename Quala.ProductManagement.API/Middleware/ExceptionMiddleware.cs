using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Text.Json;

namespace Quala.ProductManagement.API.Middleware
{
   
        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ILogger<ExceptionMiddleware> _logger;

            public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext httpContext)
            {
                try
                {
                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Excepción no controlada");
                    await HandleExceptionAsync(httpContext, ex);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                context.Response.ContentType = "application/json";

                var statusCode = exception switch
                {
                    ValidationException _ => StatusCodes.Status400BadRequest,
                    ArgumentException _ => StatusCodes.Status400BadRequest,
                    KeyNotFoundException _ => StatusCodes.Status404NotFound,
                    UnauthorizedAccessException _ => StatusCodes.Status401Unauthorized,
                    SqlException _ => StatusCodes.Status503ServiceUnavailable,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;

                var response = new
                {
                    context.Response.StatusCode,
                    Message = statusCode == StatusCodes.Status500InternalServerError
                        ? "Error de servidor"
                        : exception.Message,
                    Detailed = exception.InnerException?.Message
                };



                return context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    
}

