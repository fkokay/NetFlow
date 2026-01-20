using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class TenderConfigration : IEntityTypeConfiguration<TenderEntity>
    {
        public void Configure(EntityTypeBuilder<TenderEntity> builder)
        {
            builder.ToTable("Tender");

            builder.HasKey(x => x.Id);
        }
    }
}
