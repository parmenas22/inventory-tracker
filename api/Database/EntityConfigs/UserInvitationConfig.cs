using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Database.EntityConfigs
{
    public class UserInvitationConfig : IEntityTypeConfiguration<UserInvitation>
    {
        public void Configure(EntityTypeBuilder<UserInvitation> builder)
        {
            builder.ToTable("UserInvitation");
            builder.HasKey(invitation => invitation.UserInvitationId);
            builder.Property(invitation => invitation.Email).HasMaxLength(256).IsRequired();
            builder.Property(invitation => invitation.TokenHash).HasMaxLength(64).IsRequired();
            builder.HasIndex(invitation => invitation.TokenHash).IsUnique();
            builder.HasOne(invitation => invitation.Role)
                .WithMany()
                .HasForeignKey(invitation => invitation.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
