using MediatR;
using WinBind.Domain.Models.Basket;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetUserBasketQueryRequest : IRequest<ResponseModel<UserBasketModel>>
    {
        public Guid? UserId { get; set; }

        public GetUserBasketQueryRequest(Guid? userId)
        {
            UserId = userId;
        }
    }
}
