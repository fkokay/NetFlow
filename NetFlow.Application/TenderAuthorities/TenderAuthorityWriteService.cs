using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Tenders;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderAuthorities
{
    public class TenderAuthorityWriteService
    {
        private readonly INetFlowDbContext _db;
        public TenderAuthorityWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateTenderAuthorityRequest request)
        {
            TenderAuthorityEntity tender = new()
            {
               ParentAuthorityCode = request.ParentAuthorityCode,
               UnitCode = request.UnitCode,
               TenderId = request.TenderId,
               CreatedAt = request.CreatedAt
            };
            await _db.TenderAuthorities.AddAsync(tender);
            await _db.SaveChangesAsync();
            return tender.Id;
        }
        public async Task<int> EditAsync(EditTenderAuthorityRequest request)
        {
            var tenderAuthority = await _db.TenderAuthorities.FirstAsync(x => x.Id == request.Id);
            tenderAuthority.Id = request.Id;
            tenderAuthority.TenderId = request.TenderId;
            tenderAuthority.ParentAuthorityCode = request.ParentAuthorityCode;
            tenderAuthority.UnitCode = request.UnitCode;
            tenderAuthority.CreatedAt = request.CreatedAt;
            _db.TenderAuthorities.Update(tenderAuthority);
            await _db.SaveChangesAsync();
            return tenderAuthority.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var tenderAuthority = await _db.TenderAuthorities.FirstOrDefaultAsync(x => x.Id == id);
            if (tenderAuthority != null)
            {
                _db.TenderAuthorities.Remove(tenderAuthority);
                await _db.SaveChangesAsync();
            }
        }
    }
}
