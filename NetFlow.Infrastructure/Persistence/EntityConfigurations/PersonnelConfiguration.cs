using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class PersonnelConfiguration : IEntityTypeConfiguration<PersonnelEntity>
    {
        public void Configure(EntityTypeBuilder<PersonnelEntity> builder)
        {
            builder.ToTable("Personnel");
            builder.ToTable(tb => tb.HasTrigger("TRG_Personnel_ReActive"));
        }
    }
}
