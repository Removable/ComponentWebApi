using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComponentUtil.Common.Data;
using ComponentWebApi.Model.Articles;
using ComponentWebApi.Repository.Repositories;
using ComponentWebApi.Repository.UnitOfWorks;
using EasyCaching.Core;
using Microsoft.EntityFrameworkCore;

namespace ComponentWebApi.Services.Articles
{
    public class ArticleService : BaseService<Article, int>, IArticleService
    {
        private readonly IEasyCachingProvider _easyCaching;
        private readonly IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork, IRepository<Article, int> repository,
            IEasyCachingProvider easyCaching, IMapper mapper) : base(unitOfWork, repository)
        {
            _easyCaching = easyCaching;
            _mapper = mapper;
        }

        public async Task<PageData<ArticleVm>> GetAllArticlesTitle(int current, int rows)
        {
            const string cacheKey = "Article_IndexList";
            List<ArticleVm> list;

            //优先从缓存中获取
            var cacheData = await _easyCaching.GetAsync<List<ArticleVm>>(cacheKey);
            if (cacheData.HasValue)
                list = cacheData.Value;
            else
            {
                var data = await _repository.GetAll().ToListAsync();
                list = data.Select(i => _mapper.Map<ArticleVm>(i)).ToList();

                //存入缓存
                await _easyCaching.SetAsync(cacheKey, list, TimeSpan.FromDays(1));
            }

            var paged = new PageData<ArticleVm>(list, current, rows);
            return paged;
        }
    }
}