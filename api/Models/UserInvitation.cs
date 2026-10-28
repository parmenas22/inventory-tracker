namespace api.Models
{
    public class UserInvitation : BaseModel
    {
        public Guid UserInvitationId { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
        public string TokenHash { get; set; } = string.Empty;
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset? AcceptedAt { get; set; }
        public Role Role { get; set; } = null!;
    }
}
