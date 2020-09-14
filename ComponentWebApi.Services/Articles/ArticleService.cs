using ComponentsBlog.Repository.UnitOfWorks;
using ComponentWebApi.Model.Articles;
using ComponentWebApi.Repository.Repositories;

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
    }
}