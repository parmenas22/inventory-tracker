using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Database.EntityConfigs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c => c.CategoryId);
            builder.HasMany(c => c.Products).WithOne(c => c.Category);
            builder.HasData(
                new Category { CategoryId = Guid.NewGuid(), Name = "Electronics" },
                new Category { CategoryId = Guid.NewGuid(), Name = "Furniture" },
                new Category { CategoryId = Guid.NewGuid(), Name = "Food" },
                new Category { CategoryId = Guid.NewGuid(), Name = "Clothing" }
                );
        }
    }
}