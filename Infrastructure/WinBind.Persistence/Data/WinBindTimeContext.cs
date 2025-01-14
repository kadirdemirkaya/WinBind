using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Extensions;
using WinBind.Domain.Models.Options;

namespace WinBind.Persistence.Data
{
    public class WinBindTimeContext : IDesignTimeDbContextFactory<WinBindDbContext>
    {
        public WinBindDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}{"/../../Presentation/WinBind.Api"}")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

            DbContextOptionsBuilder<WinBindDbContext> dbContextOptionsBuilder = new();

            SqlServerOptions sqlOptions = configuration.GetOptions<SqlServerOptions>("SqlServerOptions");
            dbContextOptionsBuilder.UseNpgsql(sqlOptions.SqlConnection);

            return new(dbContextOptionsBuilder.Options);
        }
    }
}
