using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
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

            // Primary Key
            builder.HasKey(x => x.Id);

            // Foreign Key
            builder.Property(x => x.MaterialRequestId)
                   .IsRequired();

            // Item fields
            builder.Property(x => x.ItemCode)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.ItemName)
                   .HasMaxLength(200);

            builder.Property(x => x.Unit)
                   .HasMaxLength(20)
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

            builder.Property(x => x.Status)
                   .HasMaxLength(30)
                   .HasDefaultValue("Pending");

            // Date
            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            // Indexes (performance)
            builder.HasIndex(x => x.ItemCode);
            builder.HasIndex(x => x.MaterialRequestId);

            // Relationship
            builder.HasOne(x => x.MaterialRequest)
                   .WithMany(x => x.MaterialRequestItems)
                   .HasForeignKey(x => x.MaterialRequestId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
