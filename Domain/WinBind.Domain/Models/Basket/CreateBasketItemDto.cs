namespace WinBind.Domain.Models.Basket
{
    public class CreateBasketItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
