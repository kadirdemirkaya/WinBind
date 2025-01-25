using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Bid;

namespace WinBind.Application.Abstractions
{
    public interface IAuctionService
    {
        Task SendAllActiveAuctionsAsync(List<ActiveAuctionModel> activeAuctionModels);

        Task SendLastBidAtAuctionAsync(BidOnAuctionModel bidOnAuctionModel);
    }
}
