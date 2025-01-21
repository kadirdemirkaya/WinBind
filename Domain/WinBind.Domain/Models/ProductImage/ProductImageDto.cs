using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBind.Domain.Models.ProductImage
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public Guid ProductId { get; set; }
    }
}
