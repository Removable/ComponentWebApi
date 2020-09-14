using Microsoft.Extensions.DependencyInjection;

namespace ComponentWebApi.Api.Extensions
{
    public static class EasyCachingSetup
    {
        public static void AddEasyCachingInmemory(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            //Important step for In-Memory Caching
            services.AddEasyCaching(options =>
            {
                //use memory cache
                options.UseInMemory(configuration, "defaultJson", "EasyCaching:inmemory");
                
                // use memory cache with a simple way
                // options.UseInMemory("default");

                // use memory cache with your own configuration
                // options.UseInMemory(config =>
                // {
                //     // DBConfig这个是每种Provider的特有配置
                //     config.DBConfig = new InMemoryCachingOptions
                //     {
                //         // InMemory的过期扫描频率，默认值是60秒
                //         ExpirationScanFrequency = 60,
                //         // InMemory的最大缓存数量, 默认值是10000
                //         SizeLimit = 100,
                //
                //         // below two settings are added in v0.8.0
                //         // enable deep clone when reading object from cache or not, default value is true.
                //         EnableReadDeepClone = true,
                //         // enable deep clone when writing object to cache or not, default valuee is false.
                //         EnableWriteDeepClone = false,
                //     };
                //     // 预防缓存在同一时间全部失效，可以为每个key的过期时间添加一个随机的秒数，默认值是120秒
                //     config.MaxRdSecond = 120;
                //     // 是否开启日志，默认值是false
                //     config.EnableLogging = false;
                //     // 互斥锁的存活时间, 默认值是5000毫秒
                //     config.LockMs = 5000;
                //     // 没有获取到互斥锁时的休眠时间，默认值是300毫秒
                //     config.SleepMs = 300;
                // }, "myConfig");
                
                //// 读取配置文件
                //option.UseInMemory(Configuration, "myConfig");
            });
        }
    }
}