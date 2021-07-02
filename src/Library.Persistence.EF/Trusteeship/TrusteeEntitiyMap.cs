using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF.Trusteeship
{
    class TrusteeEntitiyMap : IEntityTypeConfiguration<Trustee>
    {
        public void Configure(EntityTypeBuilder<Trustee> builder)
        {
            builder.ToTable("Trusteeship");

            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(_ => _.ReturnDate).IsRequired();
            builder.Property(_ => _.DeliveryDate);

            builder.HasOne(_ => _.Person)
                 .WithMany()
                 .HasForeignKey(_ => _.PersonId);
           
            builder.HasOne(_ => _.Book)
                 .WithMany()
                 .HasForeignKey(_ => _.BookId);
        }
    }
}
