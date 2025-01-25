using WinBind.Domain.Models.ProductImage;

namespace WinBind.Domain.Models.Auction
{
    public class CreateAuctionDto
    {
        // Burası Auction için
        public Guid? UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartingPrice { get; set; }

        //public int Count { get; set; } = 1;
        //public Guid ProductId { get; set; }

        // Burası Auction da kullanılacak ve oluşturalacak Product için
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string CaseColor { get; set; }
        public string CaseShape { get; set; }
        public string BandColor { get; set; }
        public string DialColor { get; set; }
        public string Gender { get; set; }
        public string Technology { get; set; }
        public bool IsAvailable { get; set; }
        public int StockCount { get; set; }
        public List<CreateProductImageDto> ProductImages { get; set; }
        public Guid CategoryId { get; set; }
    }
}
