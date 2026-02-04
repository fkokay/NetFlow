using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceFormConfiguration : IEntityTypeConfiguration<ServiceFormEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceFormEntity> builder)
        {
            builder.ToTable("ServiceForm");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ServiceFormNo)
                .HasMaxLength(50);

            builder.Property(x => x.ServiceStatus)
           .HasConversion<byte>()
           .HasDefaultValue(ServiceStatus.Draft)
           .IsRequired();


            builder.Property(x => x.ServiceType)
                .HasConversion<byte>()
                .IsRequired();

        }
    }
}
