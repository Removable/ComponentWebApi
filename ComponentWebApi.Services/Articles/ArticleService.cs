using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentWebApi.Model.Articles;
using ComponentWebApi.Repository.Repositories;
using ComponentWebApi.Repository.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace ComponentWebApi.Services.Articles
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Article, int> _articleRepository;

        public ArticleService(IUnitOfWork unitOfWork, IRepository<Article, int> articleRepository)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Article>> GetAllArticlesTitle()
        {
            var queryable = await _articleRepository.GetAll().ToListAsync();
            
            // var list = await queryable.Paging(pageIndex, pageSize).ToListAsync();
            // return new Page<Article>(list, queryable.Count(), pageIndex, pageSize);
            return queryable;
        }

        public void DeleteSomething(int id)
        {
            System.Console.WriteLine("Handle delete something..");
        }

        public string GetCurrentUtcTime()
        {
            return System.DateTime.UtcNow.ToString();
        }

        public string PutSomething(string str)
        {
            return str;
        }
    }
}