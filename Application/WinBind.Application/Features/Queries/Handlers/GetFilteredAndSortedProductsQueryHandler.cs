using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetFilteredAndSortedProductsQueryHandler : IRequestHandler<GetFilteredAndSortedProductsQueryRequest, ResponseModel<List<ProductDto>>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public GetFilteredAndSortedProductsQueryHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ProductDto>>> Handle(GetFilteredAndSortedProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetFilteredAndSortedProducts(request);

            var mappedProducts = _mapper.Map<List<Product>>(products);

            int skip = (request.Page - 1) * request.PageSize;
            int take = request.PageSize;

            List<Product> paginatedProducts = mappedProducts
                .Skip(skip)
                .Take(take)
                .ToList();

            if (products == null)
                return new ResponseModel<List<ProductDto>>("Product list not found", 404);
            else
                return new ResponseModel<List<ProductDto>>(products);
        }
    }
}
