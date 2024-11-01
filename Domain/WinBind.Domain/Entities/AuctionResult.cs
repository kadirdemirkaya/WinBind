using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities
{
    public class AuctionResult : BaseEntity
    {
        public Guid AuctionId { get; set; }
        public decimal WinningBid { get; set; }
        public Guid WinningBidId { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime ClosedAt { get; set; }

        public virtual Auction Auction { get; set; }
        public virtual Bid WinningBidDetails { get; set; }
    }
}
