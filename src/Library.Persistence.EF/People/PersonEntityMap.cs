using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.EF.People
{
    class PersonEntityMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(_ => _.LastName).IsRequired().HasMaxLength(100);
            builder.Property(_ => _.BirthDate).IsRequired();
            builder.Property(_ => _.Address).IsRequired();
        }
    }
}
