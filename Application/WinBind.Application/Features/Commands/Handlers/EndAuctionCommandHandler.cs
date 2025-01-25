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
            // Bu kısım da auction sonlandırıldığında kazanan teklif için 
            Auction? auction = await _auctionRepo.GetAsync(a => a.Id == request.AuctionId && a.IsDeleted == false && a.AuctionStatus == Domain.Enums.AuctionStatus.Continues);

            if (auction is not null) // aktif müzayede
            {
                auction.AuctionStatus = Domain.Enums.AuctionStatus.End;

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
                                return new ResponseModel<bool>(true);

                        return new ResponseModel<bool>("AuctionResult got error while created", 400);
                    }
                }
                return new ResponseModel<bool>("Auction got error while update", 400);
            }
            return new ResponseModel<bool>("Auction is not active", 400);
        }
    }
}
