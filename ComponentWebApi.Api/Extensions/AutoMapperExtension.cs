using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ComponentWebApi.Api.Extensions
{
    /// <summary>
    /// 注入AutoMapper
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        /// 注入AutoMapper
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.Location.Contains("ComponentWebApi.Model")));

            return services;
        }
    }
}