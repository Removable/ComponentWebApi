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
        public async Task<IActionResult> Get(int type)
        {
            if (type == 1)
            {
                return Success(await _articleService.GetAll());
            }
            else if (type == 2)
            {
                _articleService.Delete(2);
                return Success();
            }
            else if (type == 3)
            {
                await _articleService.Add();
                return Success();
            }
            else
            {
                return Fail("错误");
            }
        }
    }
}