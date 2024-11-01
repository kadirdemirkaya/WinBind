using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }

        public virtual Order Order { get; set; }
    }
}
