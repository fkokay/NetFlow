using NetFlow.Application.Common.Interfaces;
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

        public async Task<bool> UpdateMaterialRequest(TenderOpexCreateMaterialRequest request, int materialRequestId, int materialRequestItemId)
        {
            var tenderOpex = await _db.TenderOpexes.FindAsync(request.TenderOpexId);
            if (tenderOpex == null)
            {
                return false;
            }
            tenderOpex.MaterialRequestId = materialRequestId;
            tenderOpex.MaterialRequestItemId = materialRequestItemId;

            _db.TenderOpexes.Update(tenderOpex);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
