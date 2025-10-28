using System.Net;
using api.DTOs;
using api.Enums;

namespace api.Helpers
{
    public class ApiResponse
    {
        public bool Succeeded { get; protected set; }
        public string? Message { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public ErrorDto Error { get; protected set; }
        public DateTime Timestamp { get; protected set; } = DateTime.Now;

        public static ApiResponse Success(HttpStatusCode statusCode, string message) => new()
        {
            Succeeded = true,
            StatusCode = statusCode,
            Message = message
        };

        public static ApiResponse Fail(HttpStatusCode statusCode, string message, Exception? ex = null, ErrorType errorType = ErrorType.UNKNOWN)
        {
            var errors = ex != null ? new ErrorDto
            {
                Message = ex.Message,
                Details = ex.InnerException?.Message,
                StackTrace = ex.StackTrace,
                ErrorType = errorType
            }
                : null;
            return new ApiResponse
            {
                Succeeded = false,
                StatusCode = statusCode,
                Error = errors,
                Message = message
            };
        }
    }

    public class ApiResponse<TValue>
    {
        public bool Succeeded { get; protected set; }
        public string? Message { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public ErrorDto Error { get; protected set; }
        public TValue Value { get; protected set; }
        public DateTime Timestamp { get; protected set; } = DateTime.Now;

        public static ApiResponse<TValue> Success(HttpStatusCode statusCode, TValue value, string message) => new()
        {
            Succeeded = true,
            StatusCode = statusCode,
            Value = value,
            Message = message
        };
        public static ApiResponse<TValue> Fail(HttpStatusCode statusCode, string message, Exception? ex = null, ErrorType errorType = ErrorType.UNKNOWN)
        {
            var error = ex != null ? new ErrorDto
            {
                Message = ex.Message,
                Details = ex.InnerException?.Message,
                StackTrace = ex.StackTrace,
                ErrorType = errorType
            }
           : null;
            return new ApiResponse<TValue>
            {
                Succeeded = false,
                StatusCode = statusCode,
                Message = message,
                Error = error
            };
        }
    }
}