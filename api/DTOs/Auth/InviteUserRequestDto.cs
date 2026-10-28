using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth
{
    public class InviteUserRequestDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public Guid RoleId { get; set; }
    }
}
