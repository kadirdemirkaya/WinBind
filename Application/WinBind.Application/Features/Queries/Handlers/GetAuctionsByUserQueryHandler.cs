using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetAuctionsByUserQueryHandler : IRequestHandler<GetAuctionsByUserQueryRequest, ResponseModel<List<GetAuctionsByUserIdDto>>>
    {
        private readonly IRepository<Auction> _repository;
        public GetAuctionsByUserQueryHandler(IRepository<Auction> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<List<GetAuctionsByUserIdDto>>> Handle(GetAuctionsByUserQueryRequest request, CancellationToken cancellationToken)
        {
            var userAuctions = await _repository.GetAllAsync(
                a => a.AppUserId == request.Id,
                false,
                a => a.Product,
                a => a.AuctionResult,
                a => a.Bids);

            if (userAuctions == null || !userAuctions.Any())
                return new ResponseModel<List<GetAuctionsByUserIdDto>>("User auctions are not found", 404);

            var auctions = userAuctions.Select(ua =>
            {
                bool isWinningBid = ua.AuctionResult.WinningBidDetails.UserId == request.Id;

                return new GetAuctionsByUserIdDto
                {
                    AuctionId = ua.Id,
                    ProductDetails = $"{ua.Product.Name} {ua.Product.Brand} {ua.Product.Model} {ua.Product.CaseColor} {ua.Product.Gender}",
                    HighestBid = ua.AuctionResult?.FinalPrice ?? 0,
                    AuctionEndDate = ua.EndDate,
                    AuctionWinnigStatus = isWinningBid
                };
            }).OrderBy(a => a.AuctionEndDate).ToList();

            return new ResponseModel<List<GetAuctionsByUserIdDto>>(auctions);
        }
    }
}
