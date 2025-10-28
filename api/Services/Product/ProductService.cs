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
                bool productExists = await _dbContext.Products.AnyAsync(p => p.Name.Equals(productRequestDto.Name, StringComparison.OrdinalIgnoreCase) && !p.IsDeleted);
                if (productExists)
                {
                    return ApiResponse<ProductResponseDto>.Fail(System.Net.HttpStatusCode.BadRequest, $"Product named {productRequestDto.Name} already exists");
                }
                //create the product
                Models.Product newProduct = new Models.Product
                {
                    Name = productRequestDto.Name,
                    Price = productRequestDto.Price,
                    Quantity = productRequestDto.Quantity,
                    CategoryId = productRequestDto.CategoryId,
                    MinStockAlert = productRequestDto.MinStockAlert,
                    CreatedBy = currentUserId
                };

                _dbContext.Products.Add(newProduct);
                await _dbContext.SaveChangesAsync();

                //fetch category name for the response
                var categoryName = await _dbContext.Categories.Where(c => c.CategoryId == newProduct.CategoryId).Select(c => c.Name).FirstOrDefaultAsync();

                var response = new ProductResponseDto
                {
                    Name = newProduct.Name,
                    Quantity = newProduct.Quantity,
                    Price = newProduct.Price,
                    MinStockAlert = newProduct.MinStockAlert,
                    Category = categoryName
                };

                return ApiResponse<ProductResponseDto>.Success(System.Net.HttpStatusCode.Created, response, "Product created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductResponseDto>.Fail(System.Net.HttpStatusCode.InternalServerError, "An error occurred while creating a new product", ex, Enums.ErrorType.PRODUCT);
            }
        }

        public async Task<ApiResponse<List<ProductResponseDto>>> GetAllProducts(ProductFilterDto filter)
        {
            try
            {
                IQueryable<Models.Product> query = _dbContext.Products
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .AsQueryable();

                //search feature
                if (!string.IsNullOrEmpty(filter.SearchTerm))
                {
                    query = query.Where(p => p.Name.Contains(filter.SearchTerm) || p.Category.Name.Contains(filter.SearchTerm));

                }

                //filter by category
                if (filter.CategoryId.HasValue)
                {
                    query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
                }

                //filter by price
                if (filter.MinPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= filter.MinPrice.Value);
                }

                if (filter.MaxPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= filter.MaxPrice.Value);
                }

                List<ProductResponseDto> products = await query.OrderBy(p => p.Name)
                                .Select(p => new ProductResponseDto
                                {
                                    Name = p.Name,
                                    Quantity = p.Quantity,
                                    Price = p.Price,
                                    MinStockAlert = p.MinStockAlert,
                                    Category = p.Category.Name,
                                }).ToListAsync();

                string message = products.Count == 0 ? "No products found" : "Products fetched successfully";

                return ApiResponse<List<ProductResponseDto>>.Success(System.Net.HttpStatusCode.OK, products, message);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ProductResponseDto>>.Fail(System.Net.HttpStatusCode.InternalServerError, "An error occurred while fetching products", ex, Enums.ErrorType.PRODUCT);
            }
        }
    }
}