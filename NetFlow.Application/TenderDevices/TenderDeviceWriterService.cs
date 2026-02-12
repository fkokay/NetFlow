using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.TenderCapexes;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Tenders;

namespace NetFlow.Application.TenderDevices
{
    public class TenderDeviceWriterService
    {
        private readonly INetFlowDbContext _db;
        public TenderDeviceWriterService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<bool> UpdateMaterialRequest(TenderDeviceCreateMaterialRequest request, int materialRequestId, int materialRequestItemId)
        {
            var tenderDevice = await _db.TenderDevices.FindAsync(request.TenderDeviceId);
            if (tenderDevice == null)
            {
                return false;
            }
            tenderDevice.MaterialRequestId = materialRequestId;
            tenderDevice.MaterialRequestItemId = materialRequestItemId;
            tenderDevice.Currency = request.Currency;
            _db.TenderDevices.Update(tenderDevice);
            await _db.SaveChangesAsync();
            return true;
        }



        public async Task<int> CreateAsync(int userId, CreateTenderDeviceRequest request)
        {
            var device = new TenderDeviceEntity
            {
                Currency = request.Currency,
                Description = request.Description,
                MaterialRequestId = request.MaterialRequestId,
                MaterialRequestItemId = request.MaterialRequestItemId,
                PurchasePrice = request.PurchasePrice,
                LinkPrice = request.LinkPrice,
                ServicePrice = request.ServicePrice,
                SupplyType = request.SupplyType,
                RentPrice = request.RentPrice,
                StockCode = request.StockCode,
                Quantity = request.Quantity,
                TenderId = request.TenderId,
                TenderAuthorityId = request.TenderAuthorityId,
                CreatedAt = request.CreatedAt,

            };
            _db.TenderDevices.Add(device);
            await _db.SaveChangesAsync();
            return device.Id;
        }

        public async Task<int> EditAsync(EditTenderDeviceRequest request)
        {
            var device = await _db.TenderDevices.FirstAsync(x => x.Id == request.Id);

            device.Currency = request.Currency;
            device.Description = request.Description;
            device.MaterialRequestId = request.MaterialRequestId;
            device.MaterialRequestItemId = request.MaterialRequestItemId;
            device.PurchasePrice = request.PurchasePrice;
            device.LinkPrice = request.LinkPrice;
            device.ServicePrice = request.ServicePrice;
            device.SupplyType = request.SupplyType;
            device.RentPrice = request.RentPrice;
            device.StockCode = request.StockCode;
            device.Quantity = request.Quantity;
            device.TenderId = request.TenderId;
            device.TenderAuthorityId = request.TenderAuthorityId;
            device.CreatedAt = request.CreatedAt;
            device.UpdatedAt = request.UpdateAt;

            _db.TenderDevices.Update(device);
            await _db.SaveChangesAsync();
            return device.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var device = await _db.TenderDevices.FirstOrDefaultAsync(x => x.Id == id);
            if (device == null) return;
            _db.TenderDevices.Remove(device);
            await _db.SaveChangesAsync();
        }
    }
}
