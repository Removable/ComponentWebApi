using System.Collections.Generic;
using System.Threading.Tasks;
using ComponentWebApi.Model.Articles;
using EasyCaching.Core.Interceptor;

namespace ComponentWebApi.Services.Articles
{
    public interface IArticleService : IBaseService
    {
        /// <summary>
        /// 获取所有博文的标题
        /// </summary>
        /// <returns></returns>
        Task<List<Article>> GetAllArticlesTitle();
        
        [EasyCachingAble(Expiration = 10)]
        string GetCurrentUtcTime();

        [EasyCachingPut(CacheKeyPrefix = "AspectCore")]
        string PutSomething(string str);

        [EasyCachingEvict(IsBefore = true)]
        void DeleteSomething(int id);
    }
}