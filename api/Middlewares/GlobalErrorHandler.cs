using System.Net;
using api.DTOs;
using api.Helpers;

namespace api.Middlewares
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandler> _logger;

        public GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;

            _logger.LogError(ex, "Unhandled exception occured.");

            var errorDetails = new List<ErrorDto>
            {
                new ErrorDto
                {
                    Message = ex.Message,
                    Details = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                }
            };

            var apiResponse = ApiResponse<object>.Fail(HttpStatusCode.InternalServerError, "An unexpected error occured", ex);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(apiResponse);
        }
    }
}