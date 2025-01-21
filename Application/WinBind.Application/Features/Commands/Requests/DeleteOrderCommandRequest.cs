using MediatR;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class DeleteOrderCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid? OrderId { get; set; }

        public DeleteOrderCommandRequest(Guid? orderId)
        {
            OrderId = orderId;
        }
    }
}
