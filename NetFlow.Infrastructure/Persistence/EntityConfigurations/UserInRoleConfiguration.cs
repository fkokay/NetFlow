using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public sealed class UserInRoleConfiguration : IEntityTypeConfiguration<UserInRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserInRoleEntity> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
