using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Persistence.Data;

namespace WinBind.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WinBindDbContext _context;
        public ProductRepository(WinBindDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetFilteredAndSortedProductsAsync(GetFilteredAndSortedProductsQueryRequest request)
        {
            var query = _context.Products.AsQueryable();

            if (request.Brand != null)
                query = query.Where(p => p.Brand == request.Brand);

            if (request.CaseColor != null)
                query = query.Where(p => p.CaseColor == request.CaseColor);

            if (request.BandColor != null)
                query = query.Where(p => p.BandColor == request.BandColor);

            if (request.SortOrder.ToLower() == "asc")
                query = query.OrderBy(p => p.Price);

            if (request.SortOrder.ToLower() == "desc")
                query = query.OrderByDescending(p => p.Price);

            var result = await query.ToListAsync();
            return result;
        }
    }
}
