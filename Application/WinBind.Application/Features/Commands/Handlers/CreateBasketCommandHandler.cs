using AutoMapper;
using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class CreateBasketCommandHandler(IRepository<Basket> _repository, IMapper _mapper) : IRequestHandler<CreateBasketCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(CreateBasketCommandRequest request, CancellationToken cancellationToken)
        {
            bool anyBasket = await _repository.AnyAsync(b => b.UserId == request.CreateBasketDto.UserId);

            if(anyBasket is false)
            {
                bool addResponse = await _repository.AddAsync(new Basket()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.CreateBasketDto.UserId ?? default,
                    BasketItems = _mapper.Map<List<BasketItem>>(request.CreateBasketDto.CreateBasketItemDtos)
                });

                if (addResponse)
                    return new ResponseModel<bool>(await _repository.SaveChangesAsync());

                return new ResponseModel<bool>("Basket could not be created", 400);
            }
            return new ResponseModel<bool>("Basket already exists", 400);
        }
    }
}
