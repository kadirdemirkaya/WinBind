using MediatR;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetAllActiveAuctionsQueryRequest : IRequest<ResponseModel<List<ActiveAuctionModel>>>
    {
    }
}
