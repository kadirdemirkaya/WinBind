namespace WinBind.Domain.Models.Basket
{
    public class UserBasketModel
    {
        public Guid UserId { get; set; }

        public List<BasketItemModel> BasketItemModel { get; set; }
    }
}
