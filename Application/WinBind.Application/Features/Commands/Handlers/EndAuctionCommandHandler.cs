using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class EndAuctionCommandHandler(IRepository<Auction> _auctionRepo, IRepository<AuctionResult> _auctionResultRepo, IRepository<Bid> _bidRepo, IRepository<Basket> _basketRepo) : IRequestHandler<EndAuctionCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(EndAuctionCommandRequest request, CancellationToken cancellationToken)
        {
            Auction? auction = await _auctionRepo.GetAsync(a => a.Id == request.AuctionId && a.IsDeleted == false);

            if (auction is not null) // aktif müzayede
            {
                auction.IsDeleted = true;

                if (_auctionRepo.Update(auction))
                {
                    bool saveAuctionResponse = await _auctionRepo.SaveChangesAsync();

                    if (saveAuctionResponse) // müzayede sonlandırıldı
                    {
                        Bid? highestBid = (await _bidRepo.GetAllAsync(b => b.AuctionId == auction.Id))
                                       .OrderByDescending(b => b.BidAmount)
                                       .FirstOrDefault();

                        if (highestBid is null || highestBid.BidAmount == 0)
                            return new ResponseModel<bool>("Auction got error while update", 400);

                        AuctionResult auctionResult = new()
                        {
                            Id = Guid.NewGuid(),
                            IsDeleted = false,
                            AuctionId = auction.Id,
                            WinningBid = highestBid.BidAmount,
                            ClosedAt = DateTime.UtcNow,
                            CreatedAtUtc = DateTime.UtcNow,
                            FinalPrice = highestBid.BidAmount,
                            WinningBidId = highestBid.Id,
                        };

                        if (await _auctionResultRepo.AddAsync(auctionResult))
                            if (await _auctionResultRepo.SaveChangesAsync()) // kişinin sepetine ekle
                            {
                                Basket? activeBasket = await _basketRepo.GetAsync(b => b.UserId == highestBid.UserId && b.IsDeleted == false, true, b => b.BasketItems);

                                if (activeBasket is not null)
                                {
                                    BasketItem basketItem = new()
                                    {
                                        BasketId = activeBasket.Id,
                                        AddedAt = DateTime.UtcNow,
                                        ProductId = auction.ProductId,
                                        Quantity = auction.Count,
                                        CreatedAtUtc = DateTime.UtcNow,
                                        IsDeleted = false,
                                    };

                                    activeBasket.BasketItems.Add(basketItem);
                                    if (await _basketRepo.SaveChangesAsync())
                                        return new ResponseModel<bool>(true);

                                    return new ResponseModel<bool>("auctions not created", 400);
                                }
                                else
                                {
                                    Basket basket = new()
                                    {
                                        UserId = highestBid.UserId,
                                        CreatedAtUtc = DateTime.UtcNow,
                                        IsDeleted = false,
                                    };
                                    BasketItem basketItem = new()
                                    {
                                        BasketId = basket.Id,
                                        AddedAt = DateTime.UtcNow,
                                        ProductId = auction.ProductId,
                                        Quantity = auction.Count,
                                        CreatedAtUtc = DateTime.UtcNow,
                                        IsDeleted = false,
                                    };
                                    basket.BasketItems.Add(basketItem);

                                    if (await _basketRepo.AddAsync(basket))
                                        if (await _basketRepo.SaveChangesAsync())
                                            return new ResponseModel<bool>(true);

                                    return new ResponseModel<bool>("auctions not created", 400);
                                }

                                //
                                // Bu kısımda web sockette tüm müzayedeleri göndermemiz gerekiyor.
                                //
                            }
                        return new ResponseModel<bool>("AuctionResult got error while created", 400);
                    }
                }
                return new ResponseModel<bool>("Auction got error while update", 400);
            }
            return new ResponseModel<bool>("Auction is not active", 400);
        }
    }
}
