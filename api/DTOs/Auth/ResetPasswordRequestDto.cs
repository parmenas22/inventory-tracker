namespace api.DTOs.Auth
{
    public class ResetPasswordRequestDto
    {
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}
