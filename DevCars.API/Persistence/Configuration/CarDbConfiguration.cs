using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configuration
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(c => c.VinCode)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(c => c.Color)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Year)
                .IsRequired();

            builder.Property(c => c.Price)
                .IsRequired()
                .HasPrecision(20,2);

            builder.Property(c => c.ProductionDate)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired()
                .HasDefaultValue(CarStatusEnum.Available);
        }
    }
}
