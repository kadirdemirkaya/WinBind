namespace WinBind.Domain.Models.Bid
{
    public class BidOnAuctionDto
    {
        public Guid AuctionId { get; set; }
        public Guid UserId { get; set; }
        public decimal BidAmount { get; set; }
    }
}
