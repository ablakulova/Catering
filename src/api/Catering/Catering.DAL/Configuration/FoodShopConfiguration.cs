﻿using Catering.DAL.Entities.Buildings;
using Catering.DAL.Entities.FoodShops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering.DAL.Configuration
{
    public class FoodShopConfiguration : IEntityTypeConfiguration<FoodShop>
    {
        public void Configure(EntityTypeBuilder<FoodShop> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.PictureUrl)
                .IsRequired();

            builder
                .Property(m => m.StreetAddress)
                .IsRequired();

            builder
                .Property(m => m.OpenTime)
                .IsRequired();

            builder
                .Property(m => m.CloseTime)
                .IsRequired();

            builder
                .HasOne(b => b.Building)
                .WithOne(a => a.FoodShop)
                .HasForeignKey<Building>(b => b.FoodShopId);

            builder
                .ToTable("FoodShops");
        }
    }
}
