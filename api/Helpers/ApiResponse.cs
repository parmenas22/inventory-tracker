using api.DTOs;

namespace api.Helpers
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public List<ErrorDto>? Errors { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public static ApiResponse<T> Success(T data, string message)
        {
            return new ApiResponse<T>
            {
                Succeeded = true,
                Message = message,
                Data = data
            };
        }
        public static ApiResponse<T> Fail(T data, string message, List<ErrorDto>? errors = null)
        {
            return new ApiResponse<T>
            {
                Succeeded = false,
                Message = message,
                Data = data,
                Errors = errors
            };
        }
    }
}