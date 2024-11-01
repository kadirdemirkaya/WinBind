using System.ComponentModel.DataAnnotations;

namespace WinBind.Domain.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
