using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WinBind.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection ApplicationServiceRegistration(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
