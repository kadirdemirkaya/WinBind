using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetAllActiveAuctionsQueryHandler(IRepository<Auction> _repository) : IRequestHandler<GetAllActiveAuctionsQueryRequest, ResponseModel<List<ActiveAuctionModel>>>
    {
        public async Task<ResponseModel<List<ActiveAuctionModel>>> Handle(GetAllActiveAuctionsQueryRequest request, CancellationToken cancellationToken)
        {
            List<Auction> auctions = await _repository.GetAllAsync(a => a.IsDeleted == false && a.EndDate > DateTime.UtcNow, false);

            List<ActiveAuctionModel> activeAuctions = auctions.Select(a => new ActiveAuctionModel
            {
                EndDate = a.EndDate,
                StartDate = a.StartDate,
                ProductId = a.ProductId,
                StartingPrice = a.StartingPrice,
                UserId = a.AppUserId
            }).OrderBy(a => a.StartDate).ToList();

            return new ResponseModel<List<ActiveAuctionModel>>(activeAuctions);
        }
    }
}
