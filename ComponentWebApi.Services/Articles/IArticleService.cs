using System.Collections.Generic;
using System.Threading.Tasks;
using ComponentWebApi.Model.Articles;

namespace ComponentWebApi.Services.Articles
{
    public interface IArticleService : IBaseService
    {
        /// <summary>
        /// 获取所有博文的标题
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        Task<List<Article>> GetAllArticlesTitle();
    }
}