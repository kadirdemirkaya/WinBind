using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Models.Product;

namespace WinBind.Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetFilteredAndSortedProducts(GetFilteredAndSortedProductsQueryRequest request)
        {
            var products = await _productRepository.GetFilteredAndSortedProductsAsync(request);
            
            var mappedProducts = _mapper.Map<List<ProductDto>>(products);

            return mappedProducts;
        }
    }
}
