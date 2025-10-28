using api.Database;
using api.DTOs.Auth;
using api.DTOs.Product;
using api.Helpers;
using api.Services.Auth;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly Guid currentUserId;
        public ProductService(AppDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
            currentUserId = Guid.Parse(_authService.GetCurrentUser());
        }
        public async Task<ApiResponse<ProductResponseDto>> AddNewProduct(ProductRequestDto productRequestDto)
        {
            try
            {
                //verify the name doesn't exist
                var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name.Equals(productRequestDto.Name, StringComparison.OrdinalIgnoreCase) && !p.IsDeleted);
                if (existingProduct is not null)
                {
                    return ApiResponse<ProductResponseDto>.Fail(System.Net.HttpStatusCode.BadRequest, "Product with this name exists");
                }
                //create the product
                Models.Product newProduct = new Models.Product
                {
                    Name = productRequestDto.Name,
                    Price = productRequestDto.Price,
                    Quantity = productRequestDto.Quantity,
                    CategoryId = productRequestDto.Category,
                    MinStockAlert = productRequestDto.MinStockAlert,
                    CreatedBy = currentUserId
                };

                _dbContext.Products.Add(newProduct);
                await _dbContext.SaveChangesAsync();

                var response = new ProductResponseDto
                {
                    Name = newProduct.Name,
                    Quantity = newProduct.Quantity,
                    Price = newProduct.Price,
                    MinStockAlert = newProduct.MinStockAlert,
                    Category = newProduct.Category.Name
                };

                return ApiResponse<ProductResponseDto>.Success(System.Net.HttpStatusCode.Created, response, "Product created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductResponseDto>.Fail(System.Net.HttpStatusCode.InternalServerError, "An error occurred while creating a new product", ex, Enums.ErrorType.PRODUCT);
            }
        }
    }
}