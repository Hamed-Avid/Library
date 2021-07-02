using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.EF.Categories
{
    class CategoryEntityMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder_)
        {
            builder_.ToTable("Categories");

            builder_.HasKey(_ => _.Id);
            builder_.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

            builder_.Property(_ => _.Title).IsRequired().IsUnicode().HasMaxLength(100);

        }
    }
}
