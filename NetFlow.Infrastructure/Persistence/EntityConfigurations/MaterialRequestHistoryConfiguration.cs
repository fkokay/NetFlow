using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class MaterialRequestHistoryConfiguration
        : IEntityTypeConfiguration<MaterialRequestHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<MaterialRequestHistoryEntity> builder)
        {
            builder.ToTable("MaterialRequestHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Action)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Notes)
                   .HasMaxLength(300);

            builder.HasOne(x => x.MaterialRequest)
                   .WithMany(x => x.MaterialRequestHistories)
                   .HasForeignKey(x => x.MaterialRequestId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
