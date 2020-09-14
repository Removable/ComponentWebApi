using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using ComponentWebApi.Api.Extensions;
using ComponentWebApi.Api.Filter;
using ComponentWebApi.Repository;
using ComponentWebApi.Repository.Repositories;
using ComponentWebApi.Repository.UnitOfWorks;
using EasyCaching.InMemory;
using EasyCaching.Interceptor.AspectCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ComponentWebApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //注入Uow依赖
            services.AddScoped<IUnitOfWork, UnitOfWork<MyDbContext>>();
            //注入泛型仓储
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            
            //依赖注入
            services.AddAutoDI();
            
            //Swagger
            services.AddSwaggerSetup();
            
            //注入DbContext
            services.AddDbContext<MyDbContext>(options => options.UseSqlite(Configuration["ConnectionStrings:Sqlite"]));

            // 設定動態代理
            services.ConfigureDynamicProxy(config => { config.Interceptors.AddTyped<ServiceAopAttribute>(Predicates.ForService("*Service")); });
            
            services.AddEasyCachingInmemory(Configuration);
            
            services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = "defaultJson");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", $"Component Web Api V1");

                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                c.RoutePrefix = "doc";
            });
            
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
