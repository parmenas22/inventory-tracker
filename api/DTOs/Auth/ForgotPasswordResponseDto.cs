namespace api.DTOs.Auth
{
    public class ForgotPasswordResponseDto
    {
        public string? ResetToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
