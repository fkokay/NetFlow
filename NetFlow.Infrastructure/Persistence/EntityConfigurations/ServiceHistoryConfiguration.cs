using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceHistoryConfiguration : IEntityTypeConfiguration<ServiceHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceHistoryEntity> builder)
        {
            builder.ToTable("ServiceHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OldStatus)
           .HasConversion<byte>()
           .IsRequired();

            builder.Property(x => x.NewStatus)
           .HasConversion<byte>()
           .IsRequired();
            
            builder.Property(x => x.ActionType)
           .HasConversion<byte>()
           .IsRequired();

        }
    }
}
