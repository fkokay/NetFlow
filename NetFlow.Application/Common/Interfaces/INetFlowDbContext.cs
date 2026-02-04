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
        DbSet<PersonnelEntity> Personnels { get; }
        DbSet<PermissionEntity> Permissions { get; }
        DbSet<UserInFirmEntity> UserInFirms { get; }
        DbSet<UserInRoleEntity> UserInRoles { get; }
        DbSet<RolePermissionEntity> RolePermissions { get; }
        DbSet<ModuleEntity> Modules { get; }
        DbSet<GuaranteeEntity> Guarantees { get; }
        DbSet<GuaranteeCommissionPeriodEntity> GuaranteeCommissionPeriods { get; }
        DbSet<GuaranteeCommissionEntity> GuaranteeCommissions { get; }
        DbSet<TenderEntity> Tenders { get; }
        DbSet<TenderOpexEntity> TenderOpexes { get; }
        DbSet<TenderCapexEntity> TenderCapexes { get; }
        DbSet<TenderReaktifEntity> TenderReaktifs { get; }
        DbSet<TenderDeviceEntity> TenderDevices { get; }
        DbSet<TenderPersonnelEntity> TenderPersonnels { get; }
        DbSet<MaterialRequestEntity> MaterialRequests { get; }
        DbSet<MaterialRequestItemEntity> MaterialRequestItems { get; }
        DbSet<MaterialRequestHistoryEntity> MaterialRequestsHistory { get; }
        DbSet<ServiceFormEntity> ServiceForms { get; }
        DbSet<ServiceFormDetailEntity> ServiceFormDetails { get; }
        DbSet<ServiceFormDocumentEntity> ServiceFormDocuments { get; }
        DbSet<ServiceFormHistoryEntity> ServiceFormHistories { get; }
        DbSet<ServiceReplacedPartEntity> ServiceReplacedParts { get; }
        DbSet<ServiceFormTechnicianEntity> ServiceFormTechnicians { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
