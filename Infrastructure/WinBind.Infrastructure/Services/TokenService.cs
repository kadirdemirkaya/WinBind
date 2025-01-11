using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WinBind.Application.Abstractions;
using WinBind.Application.Extensions;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models;
using WinBind.Domain.Models.Options;

namespace WinBind.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token GenerateToken(AppUser user)
        {
            JwtOptions jwtOptions = _configuration.GetOptions<JwtOptions>("JwtOptions");

            var siginingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Email",user.Email),
            };

            var _expries = DateTime.Now.AddMinutes(int.Parse(jwtOptions.ExpiryMinutes));

            var securityToken = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                expires: _expries,
                claims: claims,
                signingCredentials: siginingCredentials
            );

            Token token = new();
            token.Expiration = _expries;
            token.AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        public Token GenerateToken(AppUser user, string role)
        {
            JwtOptions jwtOptions = _configuration.GetOptions<JwtOptions>("JwtOptions");

            var siginingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Email",user.Email),
            };

            var _expries = DateTime.Now.AddMinutes(int.Parse(jwtOptions.ExpiryMinutes));

            var securityToken = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                expires: _expries,
                claims: claims,
                signingCredentials: siginingCredentials
            );

            Token token = new();
            token.Expiration = _expries;
            token.AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        public string GetClaimFromRequest(HttpContext httpContext, string claimType)
        {
            var authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                throw new UnauthorizedAccessException("Authorization token is missing or invalid.");
            }

            var token = authorizationHeader.Substring("Bearer ".Length); // "Bearer " kısmını çıkar
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var claimValue = securityToken.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;

            if (claimValue == null)
            {
                throw new Exception($"Claim '{claimType}' not found in token.");
            }

            return claimValue;
        }

        public string ExtractTokenFromHeader(HttpRequest request)
        {
            var authorizationHeader = request.Headers["Authorization"].FirstOrDefault();

            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }
    }
}
