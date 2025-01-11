using MediatR;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class DeleteBasketItemByBasketIdCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid BasketItemId { get; set; }
        public int DeleteCount { get; set; }

        public DeleteBasketItemByBasketIdCommandRequest(Guid basketItemId, int deleteCount)
        {
            BasketItemId = basketItemId;
            DeleteCount = deleteCount;
        }
    }
}
