using MediatR;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class StartAuctionCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid AuctionId { get; set; }

        public StartAuctionCommandRequest(Guid auctionId)
        {
            AuctionId = auctionId;
        }
    }
}
