using MediatR;
using WinBind.Domain.Models.Order;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class CreateOrderCommandRequest : IRequest<ResponseModel<CreateOrderModel>>
    {
        public CreateOrderDto CreateOrderDto { get; set; }

        public CreateOrderCommandRequest(CreateOrderDto createOrderDto)
        {
            CreateOrderDto = createOrderDto;
        }
    }
}
