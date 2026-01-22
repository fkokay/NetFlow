using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public sealed class GuaranteeConfiguration : IEntityTypeConfiguration<GuaranteeEntity>
    {
        public void Configure(EntityTypeBuilder<GuaranteeEntity> builder)
        {
            builder.ToTable("Guarantee");

            builder.Property(x => x.CommissionRate)
          .IsRequired()
          .HasPrecision(9, 4);
        }
    }
}
