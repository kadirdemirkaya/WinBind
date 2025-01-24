using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class BidOnAuctionCommandHandler(IRepository<Bid> _bidRepo) : IRequestHandler<BidOnAuctionCommandRequest, ResponseModel<bool>>
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
                            // Burada web socket'e o anki müzayedenin son amount'unu göndermeliyiz

                            return new ResponseModel<bool>(true);
                        }

                    return new ResponseModel<bool>("Bid could not be created", 400);
                }
                return new ResponseModel<bool>("Bid amount under a previous bid amount", 400);
            }
            else
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
                        // Burada web socket'e o anki müzayedenin son amount'unu göndermeliyiz

                        return new ResponseModel<bool>(true);
                    }

                return new ResponseModel<bool>("Bid could not be created", 400);
            }
        }
    }
}
