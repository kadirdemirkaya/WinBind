using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class DeleteAuctionCommandHandler(IRepository<Auction> _auctionRepo) : IRequestHandler<DeleteAuctionCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(DeleteAuctionCommandRequest request, CancellationToken cancellationToken)
        {
            Auction? auction = await _auctionRepo.GetAsync(a => a.Id == request.AuctionId && a.IsDeleted == false, true, a => a.AuctionResult, a => a.Bids, a => a.Product);

            if (auction is not null)
            {
                auction.IsDeleted = true;
                auction.UpdatedAtUtc = DateTime.UtcNow;
                auction.Product.IsDeleted = true;

                if (auction.AuctionResult is not null)
                    auction.AuctionResult.IsDeleted = true;
                if (auction.Bids is not null)
                    auction.Bids.ToList().ForEach(b => b.IsDeleted = true);

                if (_auctionRepo.Update(auction))
                    if (await _auctionRepo.SaveChangesAsync())
                        return new ResponseModel<bool>(true);

                return new ResponseModel<bool>("Auction could not be deleted", 400);
            }

            return new ResponseModel<bool>("Auction is not found", 404);
        }
    }
}
