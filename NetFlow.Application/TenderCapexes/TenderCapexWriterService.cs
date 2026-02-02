using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.TenderOpexes;
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
    }
}
