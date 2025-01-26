using MediatR;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetUserAuctionsIfBidOnAuctionQueryRequest : IRequest<ResponseModel<List<GetAuctionIfBidOnAuctionModel>>>
    {
        public Guid UserId { get; set; }

        public GetUserAuctionsIfBidOnAuctionQueryRequest(Guid userId)
        {
            UserId = userId;
        }
    }
}
