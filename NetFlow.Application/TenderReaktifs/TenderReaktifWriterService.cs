using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.TenderOpexes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderReaktifs
{
    public class TenderReaktifWriterService
    {
        private readonly INetFlowDbContext _db;
        public TenderReaktifWriterService(INetFlowDbContext db)
        {
            _db = db;
        }
        public async Task<bool> UpdateMaterialRequest(TenderReaktifCreateMaterialRequest request, int materialRequestId, int materialRequestItemId)
        {
            var tenderReaktif = await _db.TenderReaktifs.FindAsync(request.TenderReaktifId);
            if (tenderReaktif == null)
            {
                return false;
            }
            tenderReaktif.MaterialRequestId = materialRequestId;
            tenderReaktif.MaterialRequestItemId = materialRequestItemId;

            _db.TenderReaktifs.Update(tenderReaktif);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
