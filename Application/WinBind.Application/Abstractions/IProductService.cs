using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Models.Product;

namespace WinBind.Application.Abstractions
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetFilteredAndSortedProducts(GetFilteredAndSortedProductsQueryRequest request);
    }
}
