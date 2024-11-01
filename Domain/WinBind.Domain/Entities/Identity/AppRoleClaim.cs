using Microsoft.AspNetCore.Identity;
using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities.Identity
{
    public class AppRoleClaim : IdentityRoleClaim<Guid> , IBaseEntity
    {
       
    }
}
