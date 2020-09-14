using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ComponentWebApi.Api.Extensions
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = AppContext.BaseDirectory;

            var apiName = "Component Web Api";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $"{apiName} 接口文档——Netcore 3.1",
                    Description = $"{apiName} HTTP API V1",
                    Contact = new OpenApiContact
                        {Name = apiName, Email = "me@imguan.com", Url = new Uri("https://blog.imguan.com")},
                    License = new OpenApiLicense {Name = apiName, Url = new Uri("https://blog.imguan.com")}
                });
                c.OrderActionsBy(o => o.RelativePath);

                var xmlPath = Path.Combine(basePath, "ComponentWebApi.Api.xml");
                c.IncludeXmlComments(xmlPath, true);
                xmlPath = Path.Combine(basePath, "ComponentWebApi.Model.xml");
                c.IncludeXmlComments(xmlPath, true);
            });
        }
    }
}