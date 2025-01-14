using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WinBind.Application.Abstractions;
using WinBind.Application.Extensions;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models.Options;
using WinBind.Persistence.Data;
using WinBind.Persistence.Services;

namespace WinBind.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection PersistenceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlServerOptions = services.GetOptions<SqlServerOptions>("SqlServerOptions");

            services.AddDbContext<WinBindDbContext>(opt => opt.UseNpgsql(sqlServerOptions.SqlConnection));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.User.AllowedUserNameCharacters = null;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<WinBindDbContext>()
              .AddDefaultTokenProviders();

            services.Configure<UserManager<AppUser>>(userManagerOptions =>
            {
                userManagerOptions.UserValidators.Clear();
            });

            var sp = services.BuildServiceProvider();
            var context = sp.GetRequiredService<WinBindDbContext>();
            context.Database.MigrateAsync();
            context.Database.EnsureCreatedAsync();


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUserServive, UserService>();

            return services;
        }

        public static IApplicationBuilder PersistenceApplicationBuilderRegistration(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    WinBindSeedContext.SeedAsync(services.GetRequiredService<WinBindDbContext>(), services).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<WinBindDbContext>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            return app;
        }
    }
}
