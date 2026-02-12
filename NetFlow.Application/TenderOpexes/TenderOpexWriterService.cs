using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.ServiceForms;
using NetFlow.Application.ServiceHistories;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using NetFlow.Domain.Tenders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderOpexes
{
    public class TenderOpexWriterService
    {
        private readonly INetFlowDbContext _db;
        public TenderOpexWriterService(INetFlowDbContext db)
        {
            _db = db;
        }
        public async Task<int> CreateAsync(int userId, CreateTenderOpexRequest request)
        {
            var opex = new TenderOpexEntity
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
                Unit = request.Unit

            };
            _db.TenderOpexes.Add(opex);
            await _db.SaveChangesAsync();
            return opex.Id;
        }

        public async Task<int> EditAsync(EditTenderOpexRequest request)
        {
            var opex = await _db.TenderOpexes.FirstAsync(x => x.Id == request.Id);

            opex.MaterialRequestId = request.MaterialRequestId;
            opex.CreatedAt = request.CreatedAt;
            opex.Currency = request.Currency;
            opex.Description = request.Description;
            opex.MaterialRequestItemId = request.MaterialRequestItemId;
            opex.PurchasePrice = request.PurchasePrice;
            opex.Quantity = request.Quantity;
            opex.StockCode = request.StockCode;
            opex.TenderAuthorityId = request.TenderAuthorityId;
            opex.TenderId = request.TenderId;
            opex.Unit = request.Unit;

            _db.TenderOpexes.Update(opex);
            await _db.SaveChangesAsync();

            return opex.Id;
        }
        public async Task<bool> UpdateMaterialRequest(TenderOpexCreateMaterialRequest request, int materialRequestId, int materialRequestItemId)
        {
            var tenderOpex = await _db.TenderOpexes.FindAsync(request.TenderOpexId);
            if (tenderOpex == null)
            {
                return false;
            }
            tenderOpex.MaterialRequestId = materialRequestId;
            tenderOpex.MaterialRequestItemId = materialRequestItemId;
            tenderOpex.Currency = request.Currency;
            _db.TenderOpexes.Update(tenderOpex);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task DeleteAsync(int id)
        {
            var tenderOpex = await _db.TenderOpexes.FirstOrDefaultAsync(x => x.Id == id);
            if (tenderOpex == null) return;
            _db.TenderOpexes.Remove(tenderOpex);
            await _db.SaveChangesAsync();
        }
    }
}
