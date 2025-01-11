using MediatR;
using WinBind.Domain.Models.Basket;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class AddToBasketCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid? UserId { get; set; }
        public BasketItemDto BasketItemDto { get; set; }
        public AddToBasketCommandRequest(Guid? userId, BasketItemDto basketItemDto)
        {
            UserId = userId;
            BasketItemDto = basketItemDto;
        }
    }
}
