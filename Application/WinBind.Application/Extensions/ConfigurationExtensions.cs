﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WinBind.Domain.Models.Options;

namespace WinBind.Application.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            return model;
        }

        public static TModel GetOptions<TModel>(this IServiceCollection service, string section) where TModel : new()
        {
            var model = new TModel();
            var configuration = service.BuildServiceProvider().GetService<IConfiguration>();
            configuration?.GetSection(section).Bind(model);
            return model;
        }
    }
}
