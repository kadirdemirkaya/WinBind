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
using WinBind.Domain.Models.Order;
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, ResponseModel<List<ProductDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _repository;
        public GetAllProductsQueryHandler(IMapper mapper, IRepository<Product> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        async Task<ResponseModel<List<ProductDto>>> IRequestHandler<GetAllProductsQueryRequest, ResponseModel<List<ProductDto>>>.Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var productList = await _repository.GetAllAsync(p => p.IsAvailable == true && p.IsDeleted == false && p.IsAuctionProduct == false, false, p => p.ProductImages);

            if (productList == null)
                return new ResponseModel<List<ProductDto>>("Products are not found", 404);

            int skip = (request.Page - 1) * request.PageSize;
            int take = request.PageSize;

            List<Product> paginatedProducts = productList
                .Skip(skip)
                .Take(take)
                .ToList();

            var mappedProducts = _mapper.Map<List<ProductDto>>(paginatedProducts);

            return new ResponseModel<List<ProductDto>>(mappedProducts);
        }
    }
}
