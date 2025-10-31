using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Product;
using api.Helpers;
using api.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Products
{
    [ApiController]
    [Authorize]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductRequestDto productRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<ProductResponseDto>.Fail(System.Net.HttpStatusCode.BadRequest, "Invalid request"));
            }

            var result = await _productService.AddNewProduct(productRequestDto);
            if (!result.Succeeded)
            {
                return StatusCode((int)result.StatusCode, result);
            }
            return Created("api/product", result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<ProductResponseDto>.Fail(System.Net.HttpStatusCode.BadRequest, "Invalid request"));
            }

            var result = await _productService.GetAllCategories();
            if (!result.Succeeded)
            {
                return StatusCode((int)result.StatusCode, result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductFilterDto filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<ProductResponseDto>.Fail(System.Net.HttpStatusCode.BadRequest, "Invalid request"));
            }

            var result = await _productService.GetAllProducts(filter);
            if (!result.Succeeded)
            {
                return StatusCode((int)result.StatusCode, result);
            }

            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute] string productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<ProductResponseDto>.Fail(System.Net.HttpStatusCode.BadRequest, "Invalid request"));
            }
            var result = await _productService.GetProductById(productId);

            if (!result.Succeeded)
            {
                return StatusCode((int)result.StatusCode, result);
            }
            return Ok(result);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> EditProduct([FromRoute] string productId, [FromBody] ProductRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<ProductResponseDto>.Fail(System.Net.HttpStatusCode.BadRequest, "Invalid request"));
            }

            var result = await _productService.EditProduct(productId, requestDto);
            if (!result.Succeeded)
            {
                return StatusCode((int)result.StatusCode, result);
            }
            return Ok(result);
        }
    }
}