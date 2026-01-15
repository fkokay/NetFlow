using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public sealed class UserInFirmConfiguration : IEntityTypeConfiguration<UserInFirmEntity>
    {
        public void Configure(EntityTypeBuilder<UserInFirmEntity> builder)
        {
            builder.HasKey(x => new { x.UserId, x.FirmId });

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Firms)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.Firm)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.FirmId);

            builder
                .HasOne(x => x.Role)
                .WithMany(x => x.UserFirms)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
