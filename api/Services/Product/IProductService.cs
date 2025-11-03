using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Product;
using api.Helpers;

namespace api.Services.Product
{
    public interface IProductService
    {
        Task<ApiResponse<ProductResponseDto>> AddNewProduct(ProductRequestDto productRequestDto);
        Task<ApiResponse<List<ProductResponseDto>>> GetAllProducts(ProductFilterDto filter);
        Task<ApiResponse<List<CategoryDto>>> GetAllCategories();

        Task<ApiResponse<ProductResponseDto>> GetProductById(string productId);
        Task<ApiResponse<ProductResponseDto>> EditProduct(string productId, ProductRequestDto requestDto);
        Task<ApiResponse<DashboardDataDto>> GetDashboardData();

    }
}