namespace api.DTOs
{
    public class ErrorDto
    {
        public string? Message { get; set; }
        public string? ErrorType { get; set; }
        public string? StackTrace { get; set; }
        public string? Details { get; set; }
        public int? StatusCode { get; set; }
    }
}