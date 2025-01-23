using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
    }
}
