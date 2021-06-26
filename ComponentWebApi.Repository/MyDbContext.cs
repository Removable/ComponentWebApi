using ComponentWebApi.Model.Articles;
using ComponentWebApi.Model.Identity;
using ComponentWebApi.Repository.EntityMaps.Articles;
using ComponentWebApi.Repository.EntityMaps.Healthink;
using ComponentWebApi.Repository.EntityMaps.Identity;
using iHealthinkCore.Models;
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
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyIndexMenu> CompanyIndexMenus { get; set; }
        public DbSet<CompanyIndexMenuType> CompanyIndexMenuTypes { get; set; }
        public DbSet<CompanyIndexMenuTypeCompanyMapping> CompanyIndexMenuTypeCompanyMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CompanyMap());
            builder.ApplyConfiguration(new CompanyIndexMenuMap());
            builder.ApplyConfiguration(new CompanyIndexMenuTypeMap());
            builder.ApplyConfiguration(new CompanyIndexMenuTypeCompanyMappingMap());
        }
    }
}