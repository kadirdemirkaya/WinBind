using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class StartAuctionCommandHandler(IRepository<Auction> _repository) : IRequestHandler<StartAuctionCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(StartAuctionCommandRequest request, CancellationToken cancellationToken)
        {
            Auction auction = await _repository.GetAsync(a => a.Id == request.AuctionId);

            auction.AuctionStatus = Domain.Enums.AuctionStatus.Continues;
            auction.UpdatedAtUtc = DateTime.UtcNow;

            if (_repository.Update(auction))
                if (await _repository.SaveChangesAsync())
                    return new ResponseModel<bool>(true);

            return new ResponseModel<bool>("Auction could not be started", 400);
        }
    }
}
