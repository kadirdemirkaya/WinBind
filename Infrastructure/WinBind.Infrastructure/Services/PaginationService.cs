using Microsoft.AspNetCore.Http;
using WinBind.Application.Abstractions;
using WinBind.Domain.Models.Options;

namespace WinBind.Infrastructure.Services
{
    public class PaginationService(IHttpContextAccessor _httpContextAccessor) : IPaginationService
    {
        public PaginationOptions GetPaginationOptions()
            => _httpContextAccessor.HttpContext.Items["PaginationOptions"] as PaginationOptions;
    }
}
