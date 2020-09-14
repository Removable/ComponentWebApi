using ComponentWebApi.Model.Base;

namespace ComponentWebApi.Model.Articles
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article : IEntity<int>
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}