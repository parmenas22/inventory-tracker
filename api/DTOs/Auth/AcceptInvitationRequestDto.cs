using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth
{
    public class AcceptInvitationRequestDto
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [RegularExpression(
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&.#^()[\\]{}<>])[A-Za-z\\d@$!%*?&.#^()[\\]{}<>]{6,}$",
            ErrorMessage = "Password must include uppercase, lowercase, a number, and a special character.")]
        public string Password { get; set; } = string.Empty;
    }
}
