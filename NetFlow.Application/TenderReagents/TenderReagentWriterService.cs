using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.TenderOpexes;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Tenders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderReagents
{
    public class TenderReagentWriterService
    {
        private readonly INetFlowDbContext _db;
        public TenderReagentWriterService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(int userId, CreateTenderReagentRequest request)
        {
            var reagent = new TenderReagentEntity
            {
                MaterialRequestId = request.MaterialRequestId,
                CreatedAt = request.CreatedAt,
                Currency = request.Currency,
                Description = request.Description,
                MaterialRequestItemId = request.MaterialRequestItemId,
                PurchasePrice = request.PurchasePrice,
                Quantity = request.Quantity,
                StockCode = request.StockCode,
                TenderAuthorityId = request.TenderAuthorityId,
                TenderId = request.TenderId,
                SutCode = request.SutCode,
                SutPoint = request.SutPoint,
                UnitPrice = request.UnitPrice,
                TestName = request.TestName,
            };
            _db.TenderReagents.Add(reagent);
            await _db.SaveChangesAsync();
            return reagent.Id;
        }
        public async Task<int> EditAsync(EditTenderReagentRequest request)
        {
            var reagent = await _db.TenderReagents.FirstAsync(x => x.Id == request.Id);

            reagent.MaterialRequestId = request.MaterialRequestId;
            reagent.CreatedAt = request.CreatedAt;
            reagent.Currency = request.Currency;
            reagent.Description = request.Description;
            reagent.MaterialRequestItemId = request.MaterialRequestItemId;
            reagent.PurchasePrice = request.PurchasePrice;
            reagent.Quantity = request.Quantity;
            reagent.StockCode = request.StockCode;
            reagent.TenderAuthorityId = request.TenderAuthorityId;
            reagent.TenderId = request.TenderId;
            reagent.SutCode = request.SutCode;
            reagent.SutPoint = request.SutPoint;
            reagent.UnitPrice = request.UnitPrice;
            reagent.TestName = request.TestName;
            reagent.UpdatedAt = DateTime.UtcNow;
            _db.TenderReagents.Update(reagent);
            await _db.SaveChangesAsync();

            return reagent.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var tenderReagent = await _db.TenderReagents.FirstOrDefaultAsync(x => x.Id == id);
            if (tenderReagent == null) return;
            _db.TenderReagents.Remove(tenderReagent);
            await _db.SaveChangesAsync();
        }
        public async Task<bool> UpdateMaterialRequest(TenderReagentCreateMaterialRequest request, int materialRequestId, int materialRequestItemId)
        {
            var tenderReagent = await _db.TenderReagents.FindAsync(request.TenderReaktifId);
            if (tenderReagent == null)
            {
                return false;
            }
            tenderReagent.MaterialRequestId = materialRequestId;
            tenderReagent.MaterialRequestItemId = materialRequestItemId;
            tenderReagent.Currency = request.Currency;
            _db.TenderReagents.Update(tenderReagent);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
