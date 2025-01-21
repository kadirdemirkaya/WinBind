using System.Security.Principal;

namespace WinBind.Domain.Models.Order
{
    public class OrderDetailModel
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string ProductName { get; set; }
        public int Quantity{ get; set; }
    }
}
