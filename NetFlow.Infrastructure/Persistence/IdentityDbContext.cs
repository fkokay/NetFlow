using Microsoft.EntityFrameworkCore;
using NetFlow.Domain.Firms;
using NetFlow.Domain.Identity;
using NetFlow.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserInFirm
            modelBuilder.Entity<UserInFirmEntity>()
                .HasKey(x => new { x.UserId, x.FirmId });

            modelBuilder.Entity<UserInFirmEntity>()
                .HasOne(x => x.User)
                .WithMany(x => x.Firms)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserInFirmEntity>()
                .HasOne(x => x.Firm)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.FirmId);

            modelBuilder.Entity<UserInFirmEntity>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserFirms)
                .HasForeignKey(x => x.RoleId);

            // UserInRole
            modelBuilder.Entity<UserInRoleEntity>()
                .HasKey(x => new { x.UserId, x.RoleId });

            // RolePermission
            modelBuilder.Entity<RolePermissionEntity>()
                .HasKey(x => new { x.RoleId, x.PermissionId });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<FirmEntity> Firms => Set<FirmEntity>();
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<PermissionEntity> Permissions => Set<PermissionEntity>();

        public DbSet<UserInFirmEntity> UserInFirms => Set<UserInFirmEntity>();
        public DbSet<UserInRoleEntity> UserInRoles => Set<UserInRoleEntity>();
        public DbSet<RolePermissionEntity> RolePermissions => Set<RolePermissionEntity>();
    }
}
