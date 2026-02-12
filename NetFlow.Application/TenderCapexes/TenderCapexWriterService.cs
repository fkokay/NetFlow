using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.TenderOpexes;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Tenders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderCapexes
{
    public class TenderCapexWriterService
    {
        private readonly INetFlowDbContext _db;
        public TenderCapexWriterService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<bool> UpdateMaterialRequest(TenderCapexCreateMaterialRequest request, int materialRequestId, int materialRequestItemId)
        {
            var tenderCapex = await _db.TenderCapexes.FindAsync(request.TenderCapexId);
            if (tenderCapex == null)
            {
                return false;
            }
            tenderCapex.MaterialRequestId = materialRequestId;
            tenderCapex.MaterialRequestItemId = materialRequestItemId;

            _db.TenderCapexes.Update(tenderCapex);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<int> CreateAsync(int userId, CreateTenderCapexRequest request)
        {
            var capex = new TenderCapexEntity
            {
                Currency = request.Currency,
                Description = request.Description,
                MaterialRequestId = request.MaterialRequestId,
                MaterialRequestItemId = request.MaterialRequestItemId,
                PurchasePrice = request.PurchasePrice,
                Unit = request.Unit,
                StockCode = request.StockCode,
                Quantity = request.Quantity,
                TenderId = request.TenderId,
                TenderAuthorityId = request.TenderAuthorityId,
                CreatedAt = request.CreatedAt,
            };
            _db.TenderCapexes.Add(capex);
            await _db.SaveChangesAsync();
            return capex.Id;
        }

        public async Task<int> EditAsync(EditTenderCapexRequest request)
        {
            var capex = await _db.TenderCapexes.FirstAsync(x => x.Id == request.Id);

            capex.Currency = request.Currency;
            capex.Description = request.Description;
            capex.MaterialRequestId = request.MaterialRequestId;
            capex.MaterialRequestItemId = request.MaterialRequestItemId;
            capex.PurchasePrice = request.PurchasePrice;
            capex.Unit = request.Unit;
            capex.StockCode = request.StockCode;
            capex.Quantity = request.Quantity;
            capex.TenderId = request.TenderId;
            capex.TenderAuthorityId = request.TenderAuthorityId;
            capex.CreatedAt = request.CreatedAt;
            capex.UpdatedAt = request.UpdatedAt;
            _db.TenderCapexes.Update(capex);
            await _db.SaveChangesAsync();
            return capex.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var tenderCapex =await _db.TenderCapexes.FirstOrDefaultAsync(x => x.Id == id);
            if (tenderCapex == null) return;
            _db.TenderCapexes.Remove(tenderCapex);
            await _db.SaveChangesAsync();
        }
    }
}
