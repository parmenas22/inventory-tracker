namespace api.DTOs.Auth
{
    public class InvitationRoleDto
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
