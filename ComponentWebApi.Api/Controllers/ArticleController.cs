using System.Threading.Tasks;
using ComponentUtil.Webs.Controllers;
using ComponentWebApi.Model.Articles;
using ComponentWebApi.Services.Articles;
using Microsoft.AspNetCore.Mvc;

namespace ComponentWebApi.Api.Controllers
{
    /// <summary>
    /// Article控制器
    /// </summary>
    public class ArticleController : WebApiControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Success(await _articleService.GetAllArticlesTitle());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Success(await _articleService.GetByIdAsync(id));
        }

        [HttpPost("SaveNew")]
        public async Task<IActionResult> SaveNew(string title)
        {
            var article = await _articleService.SaveAsync(new Article
            {
                Title = title
            });
            return Success(article);
        }

        [HttpPost("UpdateArticle")]
        public async Task<IActionResult> UpdateArticle(int id, string title)
        {
            var article = await _articleService.GetByIdAsync(id);
            article.Title = title;
            var newArticle = await _articleService.UpdateAsync(article);
            return Success(newArticle);
        }

        [HttpPost("DelArticle")]
        public async Task<IActionResult> DelArticle(int id)
        {
            var article = await _articleService.GetByIdAsync(id);
            await _articleService.DeleteAsync(article);
            return Success();
        }
    }
}