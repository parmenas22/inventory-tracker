namespace api.Models
{
    public class User : BaseModel
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ResetToken { get; set; }
        public string? EmailLoginCodeHash { get; set; }
        public DateTimeOffset? EmailLoginCodeExpiresAt { get; set; }
        public DateTimeOffset? EmailLoginCodeSentAt { get; set; }
        public int EmailLoginCodeAttempts { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}