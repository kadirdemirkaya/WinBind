namespace WinBind.Domain.Models.Order
{
    public class GetAllOrderModel
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }

        public List<OrderDetailModel> OrderDetailModels { get; set; }
    }
}
