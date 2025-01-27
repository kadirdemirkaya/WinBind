using MediatR;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetAuctionByIdQueryRequest : IRequest<ResponseModel<ActiveAuctionModel>>
    {
        public Guid AuctionId { get; set; }

        public GetAuctionByIdQueryRequest(Guid auctionId)
        {
            AuctionId = auctionId;
        }
    }
}
