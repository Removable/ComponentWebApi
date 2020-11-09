using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentWebApi.Model.Articles;
using ComponentWebApi.Repository.Repositories;
using ComponentWebApi.Repository.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace ComponentWebApi.Services.Articles
{
    public class ArticleService : BaseService<Article, int>, IArticleService
    {
        public ArticleService(IUnitOfWork unitOfWork, IRepository<Article, int> repository) : base(unitOfWork,
            repository)
        {
        }

        public async Task<List<Article>> GetAllArticlesTitle()
        {
            var queryable = await _repository.GetAll().ToListAsync();

            // var list = await queryable.Paging(pageIndex, pageSize).ToListAsync();
            // return new Page<Article>(list, queryable.Count(), pageIndex, pageSize);
            return queryable;
        }
    }
}