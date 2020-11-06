using System;
using System.Threading.Tasks;
using AspectCore.DependencyInjection;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace ComponentWebApi.Api.Filter
{
    public class ServiceAopAttribute : AbstractInterceptorAttribute
    {
        // 自定義攔截器也可以透過 DI 注入所需服務...
        [FromServiceContext] private ILogger<ServiceAopAttribute> Logger { get; set; }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Console.WriteLine("Before call");
                await next(context); // 進入 Service 前會於此處被攔截（如果符合被攔截的規則）...
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught");
                Logger.LogError(ex.ToString()); // 記錄例外錯誤...
                throw;
            }
            finally
            {
                Console.WriteLine("After call");
            }
        }
    }
}