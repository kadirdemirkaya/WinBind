﻿using AutoMapper;
using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetUserAuctionsIfBidOnAuctionQueryHandler(IRepository<Auction> _auctionRepo, IMapper _mapper) : IRequestHandler<GetUserAuctionsIfBidOnAuctionQueryRequest, ResponseModel<List<GetAuctionIfBidOnAuctionModel>>>
    {
        public async Task<ResponseModel<List<GetAuctionIfBidOnAuctionModel>>> Handle(GetUserAuctionsIfBidOnAuctionQueryRequest request, CancellationToken cancellationToken)
        {
            List<Auction> auctions = await _auctionRepo.GetAllAsync(
                a => a.Bids.Any(b => b.UserId == request.UserId) &&
                a.AuctionStatus == Domain.Enums.AuctionStatus.End &&
                a.IsDeleted == false,
                false,
                a => a.Bids,
                a => a.Product,
                a => a.AuctionResult,
                a => a.AuctionResult.WinningBidDetails);

            if (auctions == null || !auctions.Any())
                return new ResponseModel<List<GetAuctionIfBidOnAuctionModel>>();

            List<GetAuctionIfBidOnAuctionModel> auctionModels = auctions.Select(a =>
            {
                bool isWinningBid = a.AuctionResult?.WinningBidDetails?.UserId == request.UserId;

                return new GetAuctionIfBidOnAuctionModel
                {
                    AuctionId = a.Id,
                    HighestBid = a.AuctionResult?.FinalPrice ?? 0,
                    AuctionStartDate = a.StartDate,
                    AuctionEndDate = a.EndDate,
                    StartingPrice = a.StartingPrice,
                    ProductDto = _mapper.Map<ProductDto>(a.Product),
                    AuctionEnded = a.EndDate < DateTime.UtcNow,
                    IsUserWinner = isWinningBid
                };
            }).OrderBy(a => a.AuctionEndDate).ToList();

            return new ResponseModel<List<GetAuctionIfBidOnAuctionModel>>(auctionModels);

        }
    }
}
