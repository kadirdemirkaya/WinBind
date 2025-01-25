using Microsoft.AspNetCore.SignalR;
using WinBind.Application.Abstractions;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Bid;
using WinBind.Infrastructure.Hubs;

namespace WinBind.Infrastructure.Services
{
    public class AuctionService(IHubContext<AuctionHub> _hubContext) : IAuctionService
    {
        public async Task SendAllActiveAuctionsAsync(List<ActiveAuctionModel> activeAuctionModels)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveAllActiveAuctions", activeAuctionModels);
        }

        public async Task SendLastBidAtAuctionAsync(BidOnAuctionModel bidOnAuctionModel)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveLastBidAtAuction", bidOnAuctionModel);
        }
    }
}