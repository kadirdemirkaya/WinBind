using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class DeleteBasketItemByBasketIdCommandHandler(IRepository<BasketItem> _repository) : IRequestHandler<DeleteBasketItemByBasketIdCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(DeleteBasketItemByBasketIdCommandRequest request, CancellationToken cancellationToken)
        {
            BasketItem? basketItem = await _repository.GetAsync(bi => bi.Id == request.BasketItemId && bi.IsDeleted == false);

            if (basketItem is not null)
            {
                basketItem.Quantity -= request.DeleteCount;

                if (basketItem.Quantity <= 0)
                    basketItem.IsDeleted = true;

                bool saveResponse = await _repository.SaveChangesAsync();

                return saveResponse is true ? new ResponseModel<bool>(await _repository.SaveChangesAsync()) : new ResponseModel<bool>("BasketItem could not be deleted", 400);
            }

            return new ResponseModel<bool>("BasketItem is not found in the basket", 400);
        }
    }
}
