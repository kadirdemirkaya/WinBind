using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Bid;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class BidOnAuctionCommandHandler(IRepository<Bid> _bidRepo, IRepository<Auction> _auctionRepo, IAuctionService _auctionService) : IRequestHandler<BidOnAuctionCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(BidOnAuctionCommandRequest request, CancellationToken cancellationToken)
        {
            Bid? bid = (await _bidRepo.GetAllAsync(b => b.AuctionId == request.BidOnAuctionDto.AuctionId && b.IsDeleted == false)).OrderByDescending(b => b.BidAmount).FirstOrDefault();

            if (bid is not null) // teklif var
            {
                if (bid.BidAmount < request.BidOnAuctionDto.BidAmount)
                {
                    Bid newBid = new()
                    {
                        AuctionId = request.BidOnAuctionDto.AuctionId,
                        UserId = request.BidOnAuctionDto.UserId,
                        BidAmount = request.BidOnAuctionDto.BidAmount,
                        BidDate = DateTime.UtcNow,
                        CreatedAtUtc = DateTime.UtcNow,
                    };

                    if (await _bidRepo.AddAsync(newBid))
                        if (await _bidRepo.SaveChangesAsync())
                        {
                            BidOnAuctionModel bidOnAuctionModel = new()
                            {
                                AuctionId = request.BidOnAuctionDto.AuctionId,
                                BidAmount = request.BidOnAuctionDto.BidAmount,
                                UserId = request.BidOnAuctionDto.UserId,
                            };

                            await _auctionService.SendLastBidAtAuctionAsync(bidOnAuctionModel);

                            return new ResponseModel<bool>(true);
                        }

                    return new ResponseModel<bool>("Bid could not be created", 400);
                }
                return new ResponseModel<bool>("Bid amount under a previous bid amount", 400);
            }
            else
            {
                Auction? auction = await _auctionRepo.GetAsync(a => a.Id == request.BidOnAuctionDto.AuctionId && a.IsDeleted == false);
                
                if (auction is not null && auction.StartingPrice < request.BidOnAuctionDto.BidAmount)
                {
                    Bid newBid = new()
                    {
                        AuctionId = request.BidOnAuctionDto.AuctionId,
                        UserId = request.BidOnAuctionDto.UserId,
                        BidAmount = request.BidOnAuctionDto.BidAmount,
                        BidDate = DateTime.UtcNow,
                        CreatedAtUtc = DateTime.UtcNow,
                    };

                    if (await _bidRepo.AddAsync(newBid))
                        if (await _bidRepo.SaveChangesAsync())
                        {
                            BidOnAuctionModel bidOnAuctionModel = new()
                            {
                                AuctionId = request.BidOnAuctionDto.AuctionId,
                                BidAmount = request.BidOnAuctionDto.BidAmount,
                                UserId = request.BidOnAuctionDto.UserId,
                            };

                            await _auctionService.SendLastBidAtAuctionAsync(bidOnAuctionModel);

                            return new ResponseModel<bool>(true);
                        }
                } 

                return new ResponseModel<bool>("Bid could not be created", 400);
            }
        }
    }
}
