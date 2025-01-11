namespace WinBind.Domain.Models.Basket
{
    public class CreateBasketDto
    {
        public Guid? UserId { get; set; }
        public List<CreateBasketItemDto> CreateBasketItemDtos { get; set; }
    }
}
