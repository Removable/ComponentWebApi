using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComponentUtil.Webs.Controllers;
using ComponentWebApi.Model.Articles;
using ComponentWebApi.Services.Articles;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;

namespace ComponentWebApi.Api.Controllers
{
    /// <summary>
    /// Article控制器
    /// </summary>
    public class ArticleController : WebApiControllerBase
    {
        private readonly IEasyCachingProvider _easyCachingProvider;
        private readonly IArticleService _articleService;

        /// <summary>
        /// Article控制器
        /// </summary>
        public ArticleController(IEasyCachingProvider easyCachingProvider, IArticleService articleService)
        {
            _easyCachingProvider = easyCachingProvider;
            _articleService = articleService;
        }

        /// <summary>
        /// 获取所有文章列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public virtual async Task<IActionResult> GetAll()
        {
            try
            {
                throw new Exception("ArticleController.GetAll.异常");
                //优先从缓存中获取
                var cache = await _easyCachingProvider.GetAsync<List<Article>>("Article_IndexList");
                var list = cache.HasValue ? cache.Value : await _articleService.GetAllArticlesTitle();

                return Success(list);
            }
            catch (Exception e)
            {
                throw e;
            }
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