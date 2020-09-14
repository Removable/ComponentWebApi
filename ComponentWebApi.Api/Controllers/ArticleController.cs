using System.Threading.Tasks;
using ComponentUtil.Webs.Controllers;
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
            var a = await _articleService.GetAllArticlesTitle();
            return Success(a);
        }
    }
}