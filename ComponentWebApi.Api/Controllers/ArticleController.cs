using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IEasyCachingProvider _easyCachingProvider;
        private readonly IArticleService _articleService;

        /// <summary>
        /// Article控制器
        /// </summary>
        public ArticleController(IMapper mapper, IEasyCachingProvider easyCachingProvider,
            IArticleService articleService)
        {
            _mapper = mapper;
            _easyCachingProvider = easyCachingProvider;
            _articleService = articleService;
        }

        /// <summary>
        /// 获取所有文章列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public virtual async Task<IActionResult> GetList(int current, int rows)
        {
            var list = await _articleService.GetAllArticlesTitle(current, rows);

            return Success(list);
        }

        [HttpPost("SaveNew")]
        public virtual async Task<IActionResult> SaveNew(string title)
        {
            var article = await _articleService.SaveAsync(new Article
            {
                Title = title,
                Code = Guid.NewGuid().ToString("N"),
                Content = "正文正文正文",
                ContentPreview = "正文...",
                CreationTime = DateTime.Now,
                CreatorId = "",
                LastModificationTime = DateTime.Now,
                LastModifierId = ""
            });
            return Success(article);
        }

        [HttpPost("Get")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var article = await _articleService.GetByIdAsync(id);
            return Success(_mapper.Map<ArticleVm>(article));
        }

        [HttpPost("UpdateArticle")]
        public virtual async Task<IActionResult> UpdateArticle(int id, string title)
        {
            var article = await _articleService.GetByIdAsync(id);
            article.Title = title;
            var newArticle = await _articleService.UpdateAsync(article);
            return Success(newArticle);
        }

        [HttpPost("DelArticle")]
        public virtual async Task<IActionResult> DelArticle(int id)
        {
            var article = await _articleService.GetByIdAsync(id);
            await _articleService.DeleteAsync(article);
            return Success();
        }
    }
}