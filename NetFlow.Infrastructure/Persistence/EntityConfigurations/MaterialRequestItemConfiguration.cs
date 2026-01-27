using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class MaterialRequestItemConfiguration : IEntityTypeConfiguration<MaterialRequestItemEntity>
    {
        public void Configure(EntityTypeBuilder<MaterialRequestItemEntity> builder)
        {
            builder.ToTable("MaterialRequestItem");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MaterialRequestId).IsRequired();

            builder.Property(x => x.ItemCode)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(x => x.ItemName)
            .HasMaxLength(200);

            builder.Property(x => x.Unit)
            .HasMaxLength(20)
            .IsRequired();

            builder.Property(x => x.FulfillmentType)
            .HasConversion<int>()
            .HasDefaultValue(MaterialRequestItemFulfillmentType.Undefined)
            .IsRequired();

            builder.Property(x => x.Status)
            .HasConversion<int>()
            .HasDefaultValue(MaterialRequestItemStatus.Pending)
            .IsRequired();

            builder.Property(x => x.RequestedQuantity)
            .HasPrecision(18, 4)
            .IsRequired();

            builder.Property(x => x.FulfilledQuantity)
            .HasPrecision(18, 4)
            .HasDefaultValue(0);

            builder.Property(x => x.WarehouseCode)
            .HasMaxLength(30);

            builder.Property(x => x.AlternateItemCode)
            .HasMaxLength(50);

            builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(x => x.ItemCode);
            builder.HasIndex(x => x.MaterialRequestId);
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.FulfillmentType);

            builder.HasOne(x => x.MaterialRequest)
            .WithMany(x => x.MaterialRequestItems)
            .HasForeignKey(x => x.MaterialRequestId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

