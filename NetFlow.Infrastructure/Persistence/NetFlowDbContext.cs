using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Firms;
using NetFlow.Domain.Tenders;
using NetFlow.Infrastructure.Persistence.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence
{
    public sealed class NetFlowDbContext : DbContext, INetFlowDbContext
    {
        public NetFlowDbContext(DbContextOptions<NetFlowDbContext> options)
            : base(options) { }

        public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<RoleEntity> Roles => Set<RoleEntity>();

        public DbSet<PermissionEntity> Permissions => Set<PermissionEntity>();

        public DbSet<UserInFirmEntity> UserInFirms => Set<UserInFirmEntity>();

        public DbSet<UserInRoleEntity> UserInRoles => Set<UserInRoleEntity>();

        public DbSet<RolePermissionEntity> RolePermissions => Set<RolePermissionEntity>();

        public DbSet<FirmEntity> Firms => Set<FirmEntity>();

        public DbSet<ModuleEntity> Modules => Set<ModuleEntity>();
        public DbSet<GuaranteeEntity> Guarantees => Set<GuaranteeEntity>();
        public DbSet<GuaranteeCommissionEntity> GuaranteeCommissions => Set<GuaranteeCommissionEntity>();
        public DbSet<GuaranteeCommissionPeriodEntity> GuaranteeCommissionPeriods => Set<GuaranteeCommissionPeriodEntity>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetFlowDbContext).Assembly);
        }
    }
}
