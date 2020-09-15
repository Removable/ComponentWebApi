using System;
using System.Linq;
using System.Reflection;
using ComponentWebApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ComponentWebApi.Api.Extensions
{
    public static class AutoDISetup
    {
        /// <summary>
        /// 自动注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoDI(this IServiceCollection services)
        {
            #region 自动依赖注入

            var baseType = typeof(IDependency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = System.IO.Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
            var types = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
            var implementTypes = types.Where(x => x.IsClass).ToArray();
            var interfaceTypes = types.Where(x => x.IsInterface).ToArray();
            foreach (var implementType in implementTypes)
            {
                var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                if (interfaceType != null)
                    services.AddScoped(interfaceType, implementType);
            }

            #endregion

            return services;
        }
    }
}