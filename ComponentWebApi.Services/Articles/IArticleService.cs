using System.Collections.Generic;
using System.Threading.Tasks;
using ComponentWebApi.Model.Articles;
using EasyCaching.Core.Interceptor;

namespace ComponentWebApi.Services.Articles
{
    public interface IArticleService : IBaseService<Article, int>
    {
        /// <summary>
        /// 获取所有博文的标题
        /// </summary>
        /// <returns></returns>
        Task<List<Article>> GetAllArticlesTitle();

        // [EasyCachingAble(Expiration = 20)]
        // Task<List<Article>> GetAll();
        //
        // [EasyCachingPut(CacheKeyPrefix = "Article")]
        // Task<Article> Add();
        //
        // [EasyCachingEvict(IsBefore = false)]
        // void Delete(int id);
    }
}