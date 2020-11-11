using ComponentWebApi.Model.Articles;
using ComponentWebApi.Model.Identity;
using ComponentWebApi.Repository.EntityMaps.Articles;
using ComponentWebApi.Repository.EntityMaps.Identity;
using Microsoft.EntityFrameworkCore;

namespace ComponentWebApi.Repository
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        //定义数据集合：用于创建表
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ArticleMap());
            builder.ApplyConfiguration(new UserMap());
        }
    }
}