using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth
{
    public class VerifyEmailLoginCodeRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, RegularExpression(@"^\d{6}$")]
        public string Code { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
