namespace WinBind.Domain.Models.Order
{
    public class CreateOrderModel
    {
        public Guid OrderId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
