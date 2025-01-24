using Microsoft.Identity.Client;
using WinBind.Domain.Entities.Base;
using WinBind.Domain.Entities.Identity;

namespace WinBind.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string CaseColor { get; set; }
        public string CaseShape { get; set; }
        public string BandColor { get; set; }
        public string DialColor { get; set; }
        public string Gender { get; set; }
        public string Technology { get; set; }
        public int StockCount { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsAuctionProduct { get; set; } = false;


        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
