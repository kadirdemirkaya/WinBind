using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.ProductImage;

namespace WinBind.Domain.Models.Product
{
    public class ProductDto
    {
        public Guid? Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public bool IsAvailable { get; set; }
        public List<ProductImageDto> ProductImages { get; set; }

        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
