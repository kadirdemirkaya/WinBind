using MediatR;
using WinBind.Domain.Models.Bid;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class BidOnAuctionCommandRequest : IRequest<ResponseModel<bool>>
    {
        public BidOnAuctionDto BidOnAuctionDto { get; set; }

        public BidOnAuctionCommandRequest(BidOnAuctionDto bidOnAuctionDto)
        {
            BidOnAuctionDto = bidOnAuctionDto;
        }
    }
}
