namespace WinBind.Domain.Models.Auction
{
    public class GetAuctionsByUserIdDto
    {
        public Guid AuctionId { get; set; }
        public string ProductDetails { get; set; }  // Name + Brand + Model + Color + Gender
        public decimal HighestBid { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public bool AuctionWinnigStatus { get; set; }
    }
}
