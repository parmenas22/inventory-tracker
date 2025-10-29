using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Database.EntityConfigs
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(r => r.RoleId);
            builder.HasData(new Role { RoleId = SeedConstants.AdminRoleId, Name = "Admin", CreatedBy = SeedConstants.SystemUserId }, new Role { RoleId = SeedConstants.UserRoleId, Name = "User", CreatedBy = SeedConstants.SystemUserId });
        }
    }
}