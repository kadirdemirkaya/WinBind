using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Path { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
