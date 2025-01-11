using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class DeleteBasketByIdCommandHandler(IRepository<Basket> _repository) : IRequestHandler<DeleteBasketByIdCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(DeleteBasketByIdCommandRequest request, CancellationToken cancellationToken)
        {
            Basket? basket = await _repository.GetAsync(b => b.UserId == request.UserId && b.IsDeleted == false);

            if (basket is not null)
            {
                basket.IsDeleted = true;

                bool saveResponse = await _repository.SaveChangesAsync();

                return saveResponse is true ? new ResponseModel<bool>(await _repository.SaveChangesAsync()) : new ResponseModel<bool>("Basket could not be deleted", 400);
            }

            return new ResponseModel<bool>("Basket not found", 404);
        }
    }
}
