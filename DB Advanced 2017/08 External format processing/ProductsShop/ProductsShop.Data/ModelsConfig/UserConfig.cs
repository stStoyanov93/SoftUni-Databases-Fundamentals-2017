using System;
using System.Collections.Generic;
using System.Text;

using ProductsShop.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductsShop.Data.ModelsConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode();

            builder.Property(u => u.FirstName)
                .IsRequired(false)
                .HasMaxLength(25)
                .IsUnicode();

            builder.Property(u => u.Age)
                .IsRequired(false);
        }
    }
}
