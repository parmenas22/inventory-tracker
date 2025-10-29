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
                new Category { CategoryId = SeedConstants.ElectronicsCategoryId, Name = "Electronics" },
                new Category { CategoryId = SeedConstants.FurnitureCategoryId, Name = "Furniture" },
                new Category { CategoryId = SeedConstants.FoodCategoryId, Name = "Food" },
                new Category { CategoryId = SeedConstants.ClothingCategoryId, Name = "Clothing" }
                );
        }
    }
}