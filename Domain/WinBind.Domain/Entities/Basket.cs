using WinBind.Domain.Entities.Base;
using WinBind.Domain.Entities.Identity;

namespace WinBind.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public Guid UserId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
