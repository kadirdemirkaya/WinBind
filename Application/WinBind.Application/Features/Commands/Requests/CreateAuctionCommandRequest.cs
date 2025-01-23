using MediatR;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class CreateAuctionCommandRequest : IRequest<ResponseModel<bool>>
    {
        public CreateAuctionDto CreateAuctionDto { get; set; }

        public CreateAuctionCommandRequest(CreateAuctionDto createAuctionDto)
        {
            CreateAuctionDto = createAuctionDto;
        }
    }
}
