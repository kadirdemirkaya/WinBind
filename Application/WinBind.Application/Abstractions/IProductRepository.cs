using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Product;

namespace WinBind.Application.Abstractions
{
    public interface IProductRepository
    {
        Task<List<Product>> GetFilteredAndSortedProductsAsync(GetFilteredAndSortedProductsQueryRequest request);
    }
}
