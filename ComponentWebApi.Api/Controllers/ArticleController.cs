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
        
        [HttpGet]
        public string Get(int type)
        {
            if(type == 1)
            {
                return _articleService.GetCurrentUtcTime();
            }
            else if(type == 2)
            {
                _articleService.DeleteSomething(1);
                return "ok";
            }
            else if(type == 3)
            {
                return _articleService.PutSomething("123");
            }
            else
            {
                return "other";
            }
        }
    }
}