using Microsoft.AspNetCore.SignalR;
using WinBind.Application.Abstractions;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Bid;

namespace WinBind.Infrastructure.Hubs
{
    public class AuctionHub(IRepository<Auction> _auctionRepo) : Hub
    {
        public async Task AllActiveAuctions(List<ActiveAuctionModel> activeAuctionModels)
        {
            await Clients.All.SendAsync("ReceiveAllActiveAuctions", activeAuctionModels);
        }

        public async Task LastBidAtAuction(BidOnAuctionModel bidOnAuctionModel)
        {
            await Clients.All.SendAsync("ReceiveAllActiveAuctionBind", bidOnAuctionModel);
        }
    }
}
