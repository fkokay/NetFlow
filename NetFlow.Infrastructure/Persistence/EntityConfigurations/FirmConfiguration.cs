using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Firms;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public sealed class FirmConfiguration : IEntityTypeConfiguration<FirmEntity>
    {
        public void Configure(EntityTypeBuilder<FirmEntity> builder)
        {
            builder.ToTable("Firm");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirmCode)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.FirmName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.TaxNumber)
                .HasMaxLength(20);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.FirmCode)
                .IsUnique();
        }
    }
}
