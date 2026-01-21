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

            // Primary Key
            builder.HasKey(x => x.Id);

            // Request No
            builder.Property(x => x.RequestNo)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.HasIndex(x => x.RequestNo)
                   .IsUnique();

            // Core fields
            builder.Property(x => x.CompanyId)
                   .IsRequired();

            builder.Property(x => x.RequestType)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Priority)
                   .HasDefaultValue(MaterialRequestPriority.Normal);

            builder.Property(x => x.Status)
                .HasDefaultValue(MaterialRequestStatus.Open);

            // Optional fields
            builder.Property(x => x.RequestedDepartment)
                   .HasMaxLength(100);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            builder.Property(x => x.RejectionReason)
                   .HasMaxLength(300);

            builder.Property(x => x.FulfillmentType)
                   .HasMaxLength(30);

            builder.Property(x => x.SourceReference)
                   .HasMaxLength(50);

            // Date handling
            builder.Property(x => x.RequestDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

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
