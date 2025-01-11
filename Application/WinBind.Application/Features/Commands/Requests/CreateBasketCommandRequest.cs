using MediatR;
using WinBind.Domain.Models.Basket;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class CreateBasketCommandRequest : IRequest<ResponseModel<bool>>
    {
        public CreateBasketDto CreateBasketDto { get; set; }

        public CreateBasketCommandRequest(CreateBasketDto createBasketDto)
        {
            CreateBasketDto = createBasketDto;
        }
    }
}
