using System.Net;
using System.Text.Json;

namespace POS.system.Middleware
{
    public class exceptionHandler
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<exceptionHandler> _logger;

        public exceptionHandler(RequestDelegate next, ILogger<exceptionHandler > logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred on the server.",
                Detail = exception.Message 
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}