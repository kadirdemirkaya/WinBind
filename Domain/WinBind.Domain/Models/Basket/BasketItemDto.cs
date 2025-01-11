namespace WinBind.Domain.Models.Basket
{
    public class BasketItemDto
    {
        public Guid? BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
