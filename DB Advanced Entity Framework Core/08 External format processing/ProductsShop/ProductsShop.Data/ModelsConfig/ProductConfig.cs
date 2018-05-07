using System;
using System.Collections.Generic;
using System.Text;

using ProductsShop.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductsShop.Data.ModelsConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .IsUnicode();

            builder.HasOne(p => p.Seller)
                .WithMany(u => u.SoldProducts)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasOne(p => p.Buyer)
                .WithMany(u => u.BoughtProducts)
                .HasForeignKey(p => p.BuyerId)
                .OnDelete(DeleteBehavior.Restrict); ;

        }
    }
}
