using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public byte[] ImageData { get; set; }
        public string ImageName { get; set; }
        public string MimeType { get; set; }
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
