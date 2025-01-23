namespace WinBind.Domain.Models.Auction
{
    public class CreateAuctionDto
    {
        public Guid? UserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartingPrice { get; set; }
    }
}
