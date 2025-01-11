namespace WinBind.Domain.Models.Basket
{
    public class BasketItemModel
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
