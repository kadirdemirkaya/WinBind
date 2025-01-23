using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WinBind.Application.Abstractions;
using WinBind.Application.Extensions;
using WinBind.Domain.Models.Options;
using WinBind.Infrastructure.Middlewares;
using WinBind.Infrastructure.Services;

namespace WinBind.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection InfrastructureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPaymentService, IyzicoPaymentService>();
            services.AddScoped<IPaginationService, PaginationService>();

            JwtOptions jwtOptions = services.GetOptions<JwtOptions>("JwtOptions");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
                    };
                });

            services.AddAuthorization();

            return services;
        }

        public static IApplicationBuilder InfrastructureApplicationBuilder(this IApplicationBuilder app)
        {
            app.UseMiddleware<PaginationMiddleware>();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            return app;
        }
    }
}
