using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models;

namespace WinBind.Application.Abstractions
{
    public interface ITokenService
    {
        Token GenerateToken(AppUser user);
    }
}
