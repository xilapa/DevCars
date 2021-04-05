using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configuration
{
    public class CustomerDbConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
           builder
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.FullName)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.Property(c => c.Document)
                    .IsRequired()
                    .HasMaxLength(50);
        }
    }
}
