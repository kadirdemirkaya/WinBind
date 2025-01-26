using AutoMapper;
using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetUserAuctionsIfBidOnAuctionQueryHandler(IRepository<Auction> _auctionRepo, IRepository<Order> _orderRepo, IMapper _mapper) : IRequestHandler<GetUserAuctionsIfBidOnAuctionQueryRequest, ResponseModel<List<GetAuctionIfBidOnAuctionModel>>>
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
                a => a.Product.ProductImages,
                a => a.AuctionResult,
                a => a.AuctionResult.WinningBidDetails);

            if (auctions == null || !auctions.Any())
                return new ResponseModel<List<GetAuctionIfBidOnAuctionModel>>();

            List<GetAuctionIfBidOnAuctionModel> auctionModels = auctions.Select(a =>
            {
                bool isWinningBid = a.AuctionResult?.WinningBidDetails?.UserId == request.UserId;

                Order? order = _orderRepo.GetAsync(
                    o => o.UserId == request.UserId &&
                    o.Basket.BasketItems.Any(bi => bi.ProductId == a.ProductId) &&
                    o.Payments.Any(p => p.OrderId != null),
                    false,
                    o => o.Basket,
                    o => o.Basket.BasketItems,
                    o => o.Payments
                    ).GetAwaiter().GetResult();

                return new GetAuctionIfBidOnAuctionModel
                {
                    AuctionId = a.Id,
                    HighestBid = a.AuctionResult?.FinalPrice ?? 0,
                    AuctionStartDate = a.StartDate,
                    AuctionEndDate = a.EndDate,
                    StartingPrice = a.StartingPrice,
                    ProductDto = _mapper.Map<ProductDto>(a.Product),
                    AuctionEnded = a.EndDate < DateTime.UtcNow,
                    IsUserWinner = isWinningBid,
                    IsOrderPay = (order is not null)
                };
            }).OrderBy(a => a.AuctionEndDate).ToList();

            return new ResponseModel<List<GetAuctionIfBidOnAuctionModel>>(auctionModels);
        }
    }
}
