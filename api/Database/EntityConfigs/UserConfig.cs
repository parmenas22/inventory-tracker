using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BCrypt.Net;

namespace api.Database.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.UserId);
            builder.HasData(new User { UserId = SeedConstants.SystemUserId, FirstName = "System", LastName = "User", Email = "system@user.com", Password = BCrypt.Net.BCrypt.HashPassword("Test@123") });
            builder.HasData(new User { UserId = SeedConstants.ParmenasUserId, FirstName = "Parmenas", LastName = "Macharia", Email = "machariaparmenas22@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("Admin@123") });
        }
    }
}