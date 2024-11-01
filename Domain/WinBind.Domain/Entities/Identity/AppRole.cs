using Microsoft.AspNetCore.Identity;
using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<Guid>, IBaseEntity
    {
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
