using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WinBind.Application.Abstractions;
using WinBind.Infrastructure.Middlewares;
using WinBind.Infrastructure.Services;

namespace WinBind.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IPaginationService, PaginationService>();

            return services;
        }

        public static IApplicationBuilder InfrastructureApplicationBuilder(this IApplicationBuilder app)
        {
            app.UseMiddleware<PaginationMiddleware>();

            return app;
        }
    }
}
