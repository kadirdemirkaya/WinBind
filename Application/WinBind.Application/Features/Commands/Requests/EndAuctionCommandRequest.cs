using MediatR;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class EndAuctionCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid AuctionId { get; set; }

        public EndAuctionCommandRequest(Guid auctionId)
        {
            AuctionId = auctionId;
        }
    }
}
