using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Database.EntityConfigs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.ProductId);
            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
            builder.HasData(
                new Product { Name = "Iphone 17 pro", Quantity = 25, Price = 150000, MinStockAlert = 10, CategoryId = SeedConstants.ElectronicsCategoryId },
                new Product { Name = "Lenovo ThinkPad", Quantity = 30, Price = 100000, MinStockAlert = 10, CategoryId = SeedConstants.ElectronicsCategoryId }
            );
        }
    }
}