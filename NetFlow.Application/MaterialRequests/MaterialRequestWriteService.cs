using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.GuaranteeCommissions;
using NetFlow.Application.Guarantees;
using NetFlow.Application.MaterialRequestHistories;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
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
        private readonly MaterialRequestHistoryWriteService _materialRequestHistoryWriteService;

        public MaterialRequestWriteService(INetFlowDbContext db, MaterialRequestHistoryWriteService materialRequestHistoryWriteService)
        {
            _db = db;
            _materialRequestHistoryWriteService = materialRequestHistoryWriteService;
        }

        public async Task<int> CreateAsync(int userId, CreateMaterialRequest request)
        {

            var materialRequest = new MaterialRequestEntity();
            materialRequest.FirmId = 2015;
            materialRequest.RequestedByUserId = 1;
            materialRequest.RequestDate = DateTime.UtcNow;
            materialRequest.CreatedAt = DateTime.UtcNow;
            materialRequest.CreatedByUserId = userId;
            materialRequest.RequestNo = "MR-" + DateTime.UtcNow.Ticks;
            materialRequest.RequestType = request.RequestType;
            materialRequest.RequiredDate = request.RequiredDate;
            materialRequest.Priority = request.Priority;
            materialRequest.RequestedDepartment = request.RequestedDepartment;
            materialRequest.Description = request.Description;
            materialRequest.SourceType = request.SourceType;
            materialRequest.Status = MaterialRequestStatus.PendingApproval;
            materialRequest.AssignedToUserId = 1;
            await _db.MaterialRequests.AddAsync(materialRequest);
            await _db.SaveChangesAsync();
            return materialRequest.Id;
        }

        public async Task<int> RejectionAsync(int currentUserId, RejectionMaterialRequest request)
        {
            var materialRequest = await _db.MaterialRequests
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (materialRequest == null)
                throw new Exception("Talep bulunamadı");

            materialRequest.Status = MaterialRequestStatus.Rejected;
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

            materialRequest.Status = MaterialRequestStatus.Open;
            materialRequest.ApprovalDate = DateTime.UtcNow;
            materialRequest.ApprovedByUserId = currentUserId;
            await _db.SaveChangesAsync();
            return materialRequest.Id;
        }


        public async Task<List<int>> FulFillmentAsync(int currentUserId, FulfillmentRequest request)
        {
            var updatedIds = new List<int>();
            
            foreach (var item in request.Items)
            {
                var requestItem = await _db.MaterialRequestItems
                    .FirstOrDefaultAsync(x => x.Id == item.ItemId);

                if (requestItem == null)
                    throw new Exception($"Talep Satırı bulunamadı (ItemId: {item.ItemId})");

                requestItem.FulfillmentType = item.FulfillmentType;
                requestItem.RequestedQuantity = item.RequestedQuantity;
                requestItem.FulfilledQuantity = item.FulfilledQuantity;
                requestItem.PurchaseCustomerCode = item.PurchaseCustomerCode;
                requestItem.Currency = item.Currency;
                updatedIds.Add(requestItem.Id);
            }
            await _db.SaveChangesAsync();           
            return updatedIds;
        }
    }
}
