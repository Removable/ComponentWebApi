using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AutoMapper;
using ComponentWebApi.Api.Extensions;
using ComponentWebApi.Api.Filter;
using ComponentWebApi.Repository;
using ComponentWebApi.Repository.Repositories;
using ComponentWebApi.Repository.UnitOfWorks;
using EasyCaching.Interceptor.AspectCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            services.AddCors(c =>
            {
                c.AddDefaultPolicy(policy =>
                {
                    // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    // http://127.0.0.1:1818 和 http://localhost:1818 是不一样的，尽量写两个
                    policy
                        .WithOrigins("http://localhost:3000")
                        .WithOrigins("http://127.0.0.1:3000")
                        .AllowAnyHeader() //允许任意头
                        .AllowAnyMethod(); //允许任意方法
                    // .WithExposedHeaders("act"); //允许自定义的act头信息
                });
            });

            //注入Uow依赖
            services.AddScoped<IUnitOfWork, UnitOfWork<MyDbContext>>();
            //注入泛型仓储
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddAutoMapperProfiles(Configuration);

            //依赖注入
            services.AddAutoDI();

            //Swagger
            services.AddSwaggerSetup();

            //注入DbContext
            services.AddDbContext<MyDbContext>(options => options.UseSqlite(Configuration["ConnectionStrings:Sqlite"]));

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            }).AddControllersAsServices();

            //设置动态代理
            services.ConfigureDynamicProxy(config =>
            {
                config.Interceptors.AddTyped<ServiceAopAttribute>(Predicates.ForService("*Service"));
                config.Interceptors.AddTyped<ServiceAopAttribute>(Predicates.ForService("*Controller"));
            });

            services.AddEasyCachingInmemory(Configuration);

            services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = "defaultJson");

            //JWT验证
            services.AddJwtBearer(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            // 添加日志Provider
            loggerFactory.AddLog4Net();
        }
    }
}