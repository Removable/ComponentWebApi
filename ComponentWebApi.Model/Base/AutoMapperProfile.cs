using AutoMapper;
using ComponentWebApi.Model.Articles;

namespace ComponentWebApi.Model.Base
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// AutoMapper映射配置
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleVm>();
        }
    }
}