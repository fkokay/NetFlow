using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Tenders;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderPersonnels
{
    public class TenderPersonnelWriteService
    {
        private readonly INetFlowDbContext _db;
        public TenderPersonnelWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task EditAsync(EditTenderPersonnelRequest request)
        {
            
            var oldRecords = await _db.TenderPersonnels
                .Where(x => x.TenderId == request.TenderId)
                .ToListAsync();

            _db.TenderPersonnels.RemoveRange(oldRecords);


            foreach (var personnelId in request.PersonnelIds.Distinct())
            {
                _db.TenderPersonnels.Add(new TenderPersonnelEntity
                {
                    TenderId = request.TenderId,
                    PersonnelId = personnelId
                });
            }

            await _db.SaveChangesAsync();
        }
    }
}
