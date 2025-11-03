using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Product
{
    public class DashboardDataDto
    {
        public IEnumerable<ProductResponseDto> Products { get; set; } = new List<ProductResponseDto>();
        public int TotalProducts { get; set; }
        public decimal TotalValue { get; set; }
        public int LowStockCount { get; set; }
        public int TotalCategories { get; set; }

    }
}