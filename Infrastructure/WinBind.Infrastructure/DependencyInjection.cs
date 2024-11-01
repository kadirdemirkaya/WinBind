using Microsoft.Extensions.DependencyInjection;
using WinBind.Application.Abstractions;
using WinBind.Infrastructure.Services;

namespace WinBind.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
