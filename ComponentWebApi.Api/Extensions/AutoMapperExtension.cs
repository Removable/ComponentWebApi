using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ComponentWebApi.Api.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            // 从 appsettings.json 中获取包含配置规则的程序集信息
            var assemblies = configuration["Assembly:Mapper"];

            if (string.IsNullOrEmpty(assemblies)) return services;

            var profiles = new List<Type>();

            // 获取继承的 Profile 类型信息
            var parentType = typeof(Profile);

            foreach (var item in assemblies.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries))
            {
                // 获取所有继承于 Profile 的类
                var types = Assembly.Load(item).GetTypes()
                    .Where(i => i.BaseType != null && i.BaseType.Name == parentType.Name).ToList();

                if (types.Count != 0 || types.Any())
                    profiles.AddRange(types);
            }

            // 添加映射规则
            if (profiles.Count != 0 || profiles.Any())
                services.AddAutoMapper(profiles.ToArray());

            return services;
        }
    }
}