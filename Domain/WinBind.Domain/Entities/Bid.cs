using WinBind.Domain.Entities.Base;
using WinBind.Domain.Entities.Identity;

namespace WinBind.Domain.Entities
{
    public class Bid : BaseEntity
    {
        public Guid AuctionId { get; set; }
        public Guid UserId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }

        public virtual Auction Auction { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
