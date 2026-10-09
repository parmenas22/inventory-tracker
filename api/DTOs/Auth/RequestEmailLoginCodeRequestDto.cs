using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth
{
    public class RequestEmailLoginCodeRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
