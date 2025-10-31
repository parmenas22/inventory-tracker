using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Product
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
    }
}