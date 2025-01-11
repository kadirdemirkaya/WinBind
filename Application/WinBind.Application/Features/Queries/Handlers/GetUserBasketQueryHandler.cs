using AutoMapper;
using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Basket;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetUserBasketQueryHandler(IRepository<Basket> _repository, IMapper _mapper) : IRequestHandler<GetUserBasketQueryRequest, ResponseModel<UserBasketModel>>
    {
        public async Task<ResponseModel<UserBasketModel>> Handle(GetUserBasketQueryRequest request, CancellationToken cancellationToken)
        {
            Basket? basket = await _repository.GetAsync(b => b.UserId == request.UserId && b.IsDeleted == false, false, b => b.BasketItems);

            UserBasketModel userBasketModel = _mapper.Map<UserBasketModel>(basket);

            return new ResponseModel<UserBasketModel>(userBasketModel);
        }
    }
}
