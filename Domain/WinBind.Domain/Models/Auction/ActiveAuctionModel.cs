using WinBind.Domain.Models.Product;

namespace WinBind.Domain.Models.Auction
{
    public class ActiveAuctionModel
    {
        public Guid AuctionId { get; set; }
        public Guid UserId { get; set; }
        //public Guid ProductId { get; set; }
        public ProductDto ProductDto { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartingPrice { get; set; }
    }
}
