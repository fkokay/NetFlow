using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class MaterialRequestConfiguration
            : IEntityTypeConfiguration<MaterialRequestEntity>
    {
        public void Configure(EntityTypeBuilder<MaterialRequestEntity> builder)
        {
            builder.ToTable("MaterialRequest");


            builder.HasKey(x => x.Id);

            builder.Property(x => x.RequestNo)
            .HasMaxLength(30)
            .IsRequired();


            builder.HasIndex(x => x.RequestNo)
            .IsUnique();

            builder.Property(x => x.FirmId)
            .IsRequired();

            builder.Property(x => x.RequestType)
            .HasConversion<int>()
            .IsRequired();


            builder.Property(x => x.Priority)
            .HasConversion<int>()
            .HasDefaultValue(MaterialRequestPriority.Normal)
            .IsRequired();

            builder.Property(x => x.Status)
            .HasConversion<int>()
            .HasDefaultValue(MaterialRequestStatus.Open)
            .IsRequired();

            builder.Property(x => x.SourceType)
            .HasConversion<int>()
            .HasDefaultValue(MaterialRequestSourceType.None)
            .IsRequired();


            builder.Property(x => x.RequestNo)
            .HasMaxLength(50);

            builder.Property(x => x.RequestedDepartment)
            .HasMaxLength(100);


            builder.Property(x => x.Description)
            .HasMaxLength(500);


            builder.Property(x => x.RejectionReason)
            .HasMaxLength(300);

            builder.Property(x => x.RequestDate)
            .HasDefaultValueSql("GETUTCDATE()");


            builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");


            // Indexes (rapor & performans)
            builder.HasIndex(x => x.FirmId);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.RequestType);
            builder.HasIndex(x => x.SourceType);


            // Relationships
            builder.HasMany(x => x.MaterialRequestItems)
            .WithOne(x => x.MaterialRequest)
            .HasForeignKey(x => x.MaterialRequestId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.MaterialRequestHistories)
            .WithOne(x => x.MaterialRequest)
            .HasForeignKey(x => x.MaterialRequestId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
