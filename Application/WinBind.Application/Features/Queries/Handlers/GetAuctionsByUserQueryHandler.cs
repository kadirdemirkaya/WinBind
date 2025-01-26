using AutoMapper;
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
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetAuctionsByUserQueryHandler : IRequestHandler<GetAuctionsByUserQueryRequest, ResponseModel<List<GetAuctionsByUserIdDto>>>
    {
        private readonly IRepository<Auction> _repository;
        private readonly IMapper _mapper;
        public GetAuctionsByUserQueryHandler(IRepository<Auction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<GetAuctionsByUserIdDto>>> Handle(GetAuctionsByUserQueryRequest request, CancellationToken cancellationToken)
        {
            var userAuctions = await _repository.GetAllAsync(
                a => a.AppUserId == request.UserId,
                false,
                a => a.Product,
                a => a.AuctionResult,
                a => a.Bids,
                a => a.AuctionResult.WinningBidDetails);

            if (userAuctions == null || !userAuctions.Any())
                return new ResponseModel<List<GetAuctionsByUserIdDto>>("User auctions are not found", 404);

            var auctions = userAuctions.Select(ua =>
            {
                bool isWinningBid = ua.AuctionResult?.WinningBidDetails?.UserId == request.UserId;

                return new GetAuctionsByUserIdDto
                {
                    AuctionId = ua.Id,
                    HighestBid = ua.AuctionResult?.FinalPrice ?? 0,
                    AuctionStartDate = ua.StartDate,
                    AuctionEndDate = ua.EndDate,
                    StartingPrice = ua.StartingPrice,
                    ProductDto = _mapper.Map<ProductDto>(ua.Product),
                    AuctionEnded = ua.EndDate < DateTime.UtcNow
                };
            }).OrderBy(a => a.AuctionEndDate).ToList();

            return new ResponseModel<List<GetAuctionsByUserIdDto>>(auctions);
        }
    }
}
