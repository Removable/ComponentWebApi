using System;
using ComponentWebApi.Model.Base;

namespace ComponentWebApi.Model.Articles
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article : IEntity<int>, ICreation
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 正文预览(暂定前100个字符)
        /// </summary>
        public string ContentPreview { get; set; }

        /// <summary>
        /// 文章编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 最近修改时间
        /// </summary>
        public DateTime LastModificationTime { get; set; }

        /// <summary>
        /// 最近修改人
        /// </summary>
        public string LastModifierId { get; set; }
    }

    /// <summary>
    /// 文章视图
    /// </summary>
    public class ArticleVm
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 正文预览
        /// </summary>
        public string ContentPreview { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 文章编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 最近修改时间
        /// </summary>
        public DateTime LastModificationTime { get; set; }
    }
}