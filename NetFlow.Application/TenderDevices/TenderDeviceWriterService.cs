using NetFlow.Application.Common.Interfaces;

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
    }
}
