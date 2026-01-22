using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.GuaranteeCommissions;
using NetFlow.Application.Guarantees;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace NetFlow.Application.MaterialRequests
{
    public class MaterialRequestWriteService
    {
        private readonly INetFlowDbContext _db;

        public MaterialRequestWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateMaterialRequestRequest request)
        {

            var materialRequest = new MaterialRequestEntity();
            materialRequest.FirmId = 2015;
            materialRequest.RequestedByUserId = 1;
            materialRequest.RequestDate = DateTime.UtcNow;
            materialRequest.CreateAt = DateTime.UtcNow;
            materialRequest.CreateBy = 1;
            materialRequest.RequestNo = "MR-" + DateTime.UtcNow.Ticks;
            materialRequest.RequestType = request.RequestType;
            materialRequest.RequiredDate = request.RequiredDate;
            materialRequest.Priority = Domain.Enums.MaterialRequestPriority.Normal;
            materialRequest.RequestedDepartment = request.RequestedDepartment;
            materialRequest.Description = request.Description;
            materialRequest.SourceReference = request.SourceReference;
            materialRequest.Status = Domain.Enums.MaterialRequestStatus.Open;

            _db.MaterialRequests.Add(materialRequest);
            await _db.SaveChangesAsync();
            return materialRequest.Id;
        }
    }
}
