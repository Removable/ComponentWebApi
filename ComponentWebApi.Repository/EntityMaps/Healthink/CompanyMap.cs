using iHealthinkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComponentWebApi.Repository.EntityMaps.Healthink
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("company").HasKey(i => i.Id);
        }
    }

    public class CompanyIndexMenuMap : IEntityTypeConfiguration<CompanyIndexMenu>
    {
        public void Configure(EntityTypeBuilder<CompanyIndexMenu> builder)
        {
            builder.ToTable("CompanyIndexMenu")
                .Ignore(i => i.HasChildren)
                .Ignore(i => i.Children)
                .Ignore(i => i.MenuTypeName)
                .Ignore(i => i.MenuTypeNameEng).HasKey(i => i.Id);
            ;
        }
    }

    public class CompanyIndexMenuTypeMap : IEntityTypeConfiguration<CompanyIndexMenuType>
    {
        public void Configure(EntityTypeBuilder<CompanyIndexMenuType> builder)
        {
            builder.ToTable("CompanyIndexMenuType").HasKey(i => i.Id);
        }
    }

    public class CompanyIndexMenuTypeCompanyMappingMap : IEntityTypeConfiguration<CompanyIndexMenuTypeCompanyMapping>
    {
        public void Configure(EntityTypeBuilder<CompanyIndexMenuTypeCompanyMapping> builder)
        {
            builder.ToTable("CompanyIndexMenuTypeCompanyMapping").HasKey(i => i.Id);
        }
    }
}