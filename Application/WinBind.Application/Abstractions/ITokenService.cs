using Microsoft.AspNetCore.Http;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models;

namespace WinBind.Application.Abstractions
{
    public interface ITokenService
    {
        Token GenerateToken(AppUser user);

        string GetClaimFromRequest(HttpContext httpContext, string claimType);

        string ExtractTokenFromHeader(HttpRequest request);
    }
}
