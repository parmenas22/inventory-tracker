namespace api.DTOs.Auth
{
    public class UserAccessOverviewDto
    {
        public List<ActiveUserDto> ActiveUsers { get; set; } = new();
        public List<PendingInvitationDto> PendingInvitations { get; set; } = new();
    }

    public class ActiveUserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }

    public class PendingInvitationDto
    {
        public Guid UserInvitationId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
