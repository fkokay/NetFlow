using Microsoft.EntityFrameworkCore;
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

        public async Task<int> CreateAsync(CreateMaterialRequest request)
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
            materialRequest.Priority = "Normal";
            materialRequest.RequestedDepartment = request.RequestedDepartment;
            materialRequest.Description = request.Description;
            materialRequest.SourceReference = request.SourceReference;
            materialRequest.Status = "Waiting";

            _db.MaterialRequests.Add(materialRequest);
            await _db.SaveChangesAsync();
            return materialRequest.Id;
        }

        public async Task<int> RejectionAsync(RejectionMaterialRequest request)
        {
            var materialRequest = await _db.MaterialRequests
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (materialRequest == null)
                throw new Exception("Talep bulunamadı");

            materialRequest.Status = "Rejected";
            materialRequest.RejectionReason = request.RejectionReason;

            await _db.SaveChangesAsync();
            return materialRequest.Id;
        }
        public async Task<int> ApprovedAsync(int currentUserId, int materialId)
        {
            var materialRequest = await _db.MaterialRequests
                .FirstOrDefaultAsync(x => x.Id == materialId);

            if (materialRequest == null)
                throw new Exception("Talep bulunamadı");

            materialRequest.Status = "Open";
            materialRequest.ApprovalDate= DateTime.UtcNow;
            materialRequest.ApprovedByUserId = currentUserId;

            await _db.SaveChangesAsync();
            return materialRequest.Id;
        }
    }
}
