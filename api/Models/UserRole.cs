namespace api.Models
{
    public class UserRole : BaseModel
    {
        public Guid UserRoleId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }

    }
}