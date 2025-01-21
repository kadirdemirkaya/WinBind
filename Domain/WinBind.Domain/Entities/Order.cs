using WinBind.Domain.Entities.Base;
using WinBind.Domain.Entities.Identity;

namespace WinBind.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public Guid BasketId { get; set; }

        public virtual Basket Basket { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
