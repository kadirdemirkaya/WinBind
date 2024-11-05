using Microsoft.AspNetCore.Http;
using WinBind.Domain.Models.Options;

namespace WinBind.Infrastructure.Middlewares
{
    public class PaginationMiddleware
    {
        private readonly RequestDelegate _next;

        public PaginationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var paginationOptions = new PaginationOptions();

            if (context.Request.Headers.TryGetValue("X-Page-Number", out var pageNumber) && int.TryParse(pageNumber, out int pageNum))
            {
                paginationOptions.PageNumber = pageNum;
            }

            if (context.Request.Headers.TryGetValue("X-Page-Size", out var pageSize) && int.TryParse(pageSize, out int pageSizeNum))
            {
                paginationOptions.PageSize = pageSizeNum;
            }

            context.Items["PaginationOptions"] = paginationOptions;

            await _next(context);
        }
    }
}
