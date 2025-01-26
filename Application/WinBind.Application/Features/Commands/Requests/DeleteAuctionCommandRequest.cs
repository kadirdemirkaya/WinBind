using MediatR;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class DeleteAuctionCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid AuctionId { get; set; }

        public DeleteAuctionCommandRequest(Guid auctionId)
        {
            AuctionId = auctionId;
        }
    }
}
