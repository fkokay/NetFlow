using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceFormDocumentConfiguration : IEntityTypeConfiguration<ServiceFormDocumentEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceFormDocumentEntity> builder)
        {
            builder.ToTable("ServiceFormDocument");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageType)
            .HasConversion<int>()
            .HasDefaultValue(ImageType.Unknown)
            .IsRequired();

        }
    }
}
