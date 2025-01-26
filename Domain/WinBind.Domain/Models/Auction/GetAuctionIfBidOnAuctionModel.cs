using WinBind.Domain.Models.Product;

namespace WinBind.Domain.Models.Auction
{
    public class GetAuctionIfBidOnAuctionModel
    {
        public Guid AuctionId { get; set; }
        public ProductDto ProductDto { get; set; }
        public decimal HighestBid { get; set; }
        public decimal StartingPrice { get; set; }
        public bool AuctionEnded { get; set; }
        public DateTime AuctionStartDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public bool IsUserWinner { get; set; }
        public bool IsOrderPay { get; set; }
    }
}
