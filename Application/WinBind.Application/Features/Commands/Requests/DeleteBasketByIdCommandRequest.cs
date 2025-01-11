using MediatR;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class DeleteBasketByIdCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid? UserId { get; set; }

        public DeleteBasketByIdCommandRequest(Guid? userId)
        {
            UserId = userId;
        }
    }
}
