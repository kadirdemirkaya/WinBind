using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities
{
    public class Auction : BaseEntity
    {
        public Guid ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartingPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual AuctionResult AuctionResult { get; set; }
    }

}
