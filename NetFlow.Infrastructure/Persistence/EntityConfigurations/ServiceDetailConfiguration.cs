using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceDetailConfiguration : IEntityTypeConfiguration<ServiceDetailEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceDetailEntity> builder)
        {
            builder.ToTable("ServiceDetail");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.DetailType)
           .HasConversion<byte>()
           .IsRequired();

        }
    }
}
