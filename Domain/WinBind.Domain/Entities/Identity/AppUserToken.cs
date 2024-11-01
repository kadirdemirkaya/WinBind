using Microsoft.AspNetCore.Identity;
using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities.Identity
{
    public class AppUserToken : IdentityUserToken<Guid>, IBaseEntity
    {
       
    }
}
