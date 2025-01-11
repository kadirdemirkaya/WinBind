using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class AddToBasketCommandHandler(IRepository<Basket> _basketRepository,IRepository<BasketItem> _basketItemRepository) : IRequestHandler<AddToBasketCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(AddToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            bool saveResponse = false;
            Basket basket = await _basketRepository.GetAsync(b => b.UserId == request.UserId && b.IsDeleted == false, true, b => b.BasketItems);

            if (basket is not null)
            {
                if (basket.BasketItems.Any(b => b.ProductId == request.BasketItemDto.ProductId))
                {
                    basket.BasketItems.First(b => b.ProductId == request.BasketItemDto.ProductId).Quantity += request.BasketItemDto.Quantity;
                   
                    saveResponse = await _basketRepository.SaveChangesAsync();

                    return saveResponse is true ? new ResponseModel<bool>(true) : new ResponseModel<bool>("Basket could not be updated", 400);
                }
                else
                {
                    var newBasketItem = new BasketItem
                    {
                        Id = Guid.NewGuid(),
                        AddedAt = DateTime.UtcNow,
                        BasketId = basket.Id,
                        CreatedAtUtc = DateTime.UtcNow,
                        IsDeleted = false,
                        ProductId = request.BasketItemDto.ProductId,
                        Quantity = request.BasketItemDto.Quantity,
                    };

                    await _basketItemRepository.AddAsync(newBasketItem);

                    saveResponse = await _basketItemRepository.SaveChangesAsync();

                    return saveResponse is true ? new ResponseModel<bool>(true) : new ResponseModel<bool>("Basket could not be updated", 400);
                }
            }
            return new ResponseModel<bool>("Basket not found", 404);
        }
    }
}
