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

            ;

            // Relationships

            builder.HasMany(x => x.ServiceFormDetails)
           .WithOne(x => x.ServiceForm)
           .HasForeignKey(x => x.ServiceFormId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ServiceFormHistories)
            .WithOne(x => x.ServiceForm)
            .HasForeignKey(x => x.ServiceFormId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ServiceFormDocuments)
            .WithOne(x => x.ServiceForm)
            .HasForeignKey(x => x.ServiceFormId)
            .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
