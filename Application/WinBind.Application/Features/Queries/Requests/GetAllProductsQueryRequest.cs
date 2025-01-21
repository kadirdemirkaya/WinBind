using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetAllProductsQueryRequest : IRequest<ResponseModel<List<ProductDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetAllProductsQueryRequest(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
