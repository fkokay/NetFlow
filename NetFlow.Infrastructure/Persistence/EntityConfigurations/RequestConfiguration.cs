using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<RequestEntity>
    {
        public void Configure(EntityTypeBuilder<RequestEntity> builder)
        {
            builder.ToTable("Request");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirmId)
                   .IsRequired();

            builder.Property(x => x.RequestType)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Priority)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Subject)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .IsRequired();

            builder.Property(x => x.RelatedId)
                   .IsRequired();

            builder.Property(x => x.DueDate)
                   .IsRequired(false);

            builder.Property(x => x.ClosedAt)
                   .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();
        }
    }
}
