using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.MaterialRequestItems;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.MaterialRequestHistories
{
    public class MaterialRequestHistoryWriteService
    {
        private readonly INetFlowDbContext _db;

        public MaterialRequestHistoryWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateMaterialRequestHistoryRequest request)
        {
            var materialRequestHistory = new Domain.Entities.MaterialRequestHistoryEntity();
            materialRequestHistory.MaterialRequestId = request.MaterialRequestId;
            materialRequestHistory.ActionByUserId = request.ActionByUserId;
            materialRequestHistory.Notes = request.Notes;
            materialRequestHistory.ActionDate = request.ActionDate;
            materialRequestHistory.Action = request.Action;
            _db.MaterialRequestsHistory.Add(materialRequestHistory);
            await _db.SaveChangesAsync();
            return materialRequestHistory.Id;

        }
        public async Task<int> EditAsync(EditMaterialRequestHistoryRequest request)
        {
            var materialRequestHistory = await _db.MaterialRequestsHistory.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new Exception("Kayıt bulunamadı");

            materialRequestHistory.MaterialRequestId = request.MaterialRequestId;
            materialRequestHistory.ActionByUserId = request.ActionByUserId;
            materialRequestHistory.Notes = request.Notes;
            materialRequestHistory.ActionDate = request.ActionDate;
            materialRequestHistory.Action=request.Action;
            await _db.SaveChangesAsync();
            return materialRequestHistory.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var materialRequestHistory = await _db.MaterialRequestsHistory.FirstOrDefaultAsync(x => x.Id == id);
            _db.MaterialRequestsHistory.Remove(materialRequestHistory);
            await _db.SaveChangesAsync();
        }
    }
}
