using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceFormHistoryConfiguration : IEntityTypeConfiguration<ServiceFormHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceFormHistoryEntity> builder)
        {
            builder.ToTable("ServiceFormHistory");

            builder.HasKey(x => x.Id);


            builder.Property(x => x.OldStatus)
           .HasConversion<int>()
           .HasDefaultValue(ServiceStatus.Draft)
           .IsRequired();

            builder.Property(x => x.NewStatus)
           .HasConversion<int>()
           .HasDefaultValue(ServiceStatus.Draft)
           .IsRequired();
            
            builder.Property(x => x.ActionType)
           .HasConversion<int>()
           .HasDefaultValue(ServiceActionType.Undefined)
           .IsRequired();

        }
    }
}
