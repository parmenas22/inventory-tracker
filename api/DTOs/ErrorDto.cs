using api.Enums;

namespace api.DTOs
{
    public class ErrorDto
    {
        public string? Message { get; set; }
        public ErrorType ErrorType { get; set; }
        public string? StackTrace { get; set; }
        public string? Details { get; set; }
    }
}