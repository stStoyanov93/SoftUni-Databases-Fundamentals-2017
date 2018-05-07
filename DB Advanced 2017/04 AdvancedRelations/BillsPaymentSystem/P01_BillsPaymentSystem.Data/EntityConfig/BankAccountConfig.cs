using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(ba => ba.BankAccountId);

            builder.Property(ba => ba.BankName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(ba => ba.SwiftCode)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false);

            builder.Ignore(c => c.PaymentMethodId);
        }
    }
}
