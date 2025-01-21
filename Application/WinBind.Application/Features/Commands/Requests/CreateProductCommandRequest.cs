using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.ProductImage;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class CreateProductCommandRequest : IRequest<ResponseModel<bool>>
    {
        public ProductDto ProductDto { get; set; }
        public List<ProductImageDto> ProductImagesDto { get; set; }
    }
}
