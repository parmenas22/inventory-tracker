namespace api.Models
{
    public class UserRole : BaseModel
    {
        public Guid UserRoleId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

    }
}