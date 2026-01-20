using Microsoft.EntityFrameworkCore;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Firms;
using NetFlow.Domain.Tenders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Common.Interfaces
{
    public interface INetFlowDbContext
    {
        DbSet<UserEntity> Users { get; }
        DbSet<FirmEntity> Firms { get; }
        DbSet<RoleEntity> Roles { get; }
        DbSet<PermissionEntity> Permissions { get; }
        DbSet<UserInFirmEntity> UserInFirms { get; }
        DbSet<UserInRoleEntity> UserInRoles { get; }
        DbSet<RolePermissionEntity> RolePermissions { get; }
        DbSet<ModuleEntity> Modules { get; }
        DbSet<GuaranteeEntity> Guarantees { get; }
        DbSet<GuaranteeCommissionPeriodEntity> GuaranteeCommissionPeriods { get; }
        DbSet<GuaranteeCommissionEntity> GuaranteeCommissions { get; }
        DbSet<TenderEntity> Tenders { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
