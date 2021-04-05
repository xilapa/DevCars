﻿using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Persistence.Configuration
{
    public class OrderDbConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasMany(o => o.ExtraItems)
                .WithOne()
                .HasForeignKey(e => e.IdOrder)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(o => o.Car)
                .WithOne()
                .HasForeignKey<Order>(o => o.IdCar)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.TotalCost)
                .IsRequired()
                .HasPrecision(20, 2);


        }
    }
}
