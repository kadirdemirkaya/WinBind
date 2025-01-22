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
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(request.Brand))
                query = query.Where(p => p.Brand == request.Brand);

            if (!string.IsNullOrEmpty(request.CaseColor))
                query = query.Where(p => p.CaseColor == request.CaseColor);

            if (!string.IsNullOrEmpty(request.BandColor))
                query = query.Where(p => p.BandColor == request.BandColor);

            if (!string.IsNullOrEmpty(request.SortOrder))
            {
                if (request.SortOrder.ToLower() == "asc")
                    query = query.OrderBy(p => p.Price);

                if (request.SortOrder.ToLower() == "desc")
                    query = query.OrderByDescending(p => p.Price);
            }

            return await query.ToListAsync();
        }
    }
}