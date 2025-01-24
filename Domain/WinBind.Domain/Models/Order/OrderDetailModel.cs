using System.Security.Principal;

namespace WinBind.Domain.Models.Order
{
    public class OrderDetailModel
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity{ get; set; }

        public string Description { get; set; }
        public string SKU { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string CaseColor { get; set; }
        public string CaseShape { get; set; }
        public string BandColor { get; set; }
        public string DialColor { get; set; }
        public string Gender { get; set; }
        public string Technology { get; set; }
    }
}
