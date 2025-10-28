using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Product
{
    public class ProductRequestDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int MinStockAlert { get; set; }
    }
}