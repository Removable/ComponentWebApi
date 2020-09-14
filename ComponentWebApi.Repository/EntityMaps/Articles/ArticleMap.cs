using ComponentWebApi.Model.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComponentWebApi.Repository.EntityMaps.Articles
{
    public class ArticleMap: IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("article").HasKey(i => i.Id);
        }
    }
}