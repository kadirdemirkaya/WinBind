using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Order;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetAllOrdersQueryHandler(IRepository<Order> _repository, IMapper _mapper) : IRequestHandler<GetAllOrdersQueryRequest, ResponseModel<List<GetAllOrderModel>>>
    {
        public async Task<ResponseModel<List<GetAllOrderModel>>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _repository.Table
                .Include(o => o.Basket)                        
                    .ThenInclude(b => b.BasketItems)               
                    .ThenInclude(bi => bi.Product)                 
                .Where(o => o.UserId == request.UserId)        
                .ToListAsync();

            int skip = (request.Page - 1) * request.PageSize;
            int take = request.PageSize;

            List<Order> paginatedOrders = orders
                .Skip(skip)
                .Take(take)
                .ToList();

            List<GetAllOrderModel> getAllOrderModel = new();
            foreach (var order in paginatedOrders)
            {
                var orderDetails = order.Basket?.BasketItems?.Select(basketItem => new OrderDetailModel
                {
                    ProductId = basketItem.ProductId,
                    UserId = request.UserId ?? Guid.Empty,   
                    ProductName = basketItem.Product?.Name,  
                    Quantity = basketItem.Quantity
                }).ToList() ?? new List<OrderDetailModel>();

                getAllOrderModel.Add(new GetAllOrderModel
                {
                    UserId = request.UserId ?? Guid.Empty,  
                    OrderId = order.Id,                    
                    OrderDetailModels = orderDetails      
                });
            }

            return new ResponseModel<List<GetAllOrderModel>>(getAllOrderModel);
        }
    }
}
