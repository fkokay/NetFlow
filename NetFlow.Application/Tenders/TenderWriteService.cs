using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Tenders
{
    public class TenderWriteService
    {
        private readonly INetFlowDbContext _db;
        public TenderWriteService(INetFlowDbContext db)
        {
            _db = db;
        }
      
        public async Task<int> EditAsync(EditTenderRequest request)
        {
            var tender = await _db.Tenders.FirstAsync(x => x.Id == request.Id);
            tender.Id = request.Id;
            tender.FirmId = request.FirmId;
            tender.FirmName = request.FirmName;
            tender.TenderCode = request.TenderCode;
            tender.TenderName = request.TenderName;
            tender.PublicAuthorityCode = request.PublicAuthorityCode;
            tender.PublicAuthorityName = request.PublicAuthorityName;
            tender.TenderType = request.TenderType;
            tender.TenderMethod = request.TenderMethod;
            tender.TenderStartDate = request.TenderStartDate;
            tender.TenderEndDate = request.TenderEndDate;
            tender.TenderDueDate = request.TenderDueDate;
            tender.TenderQuantity = request.TenderQuantity;
            tender.TenderAmount = request.TenderAmount;
            tender.Currency = request.Currency;
            tender.TemporaryGuaranteeRateId = request.TemporaryGuaranteeRateId;
            tender.TemporaryGuaranteeSubject = request.TemporaryGuaranteeSubject;
            tender.FinalGuaranteeRateId = request.FinalGuaranteeRateId;
            tender.FinalGuaranteeSubject = request.FinalGuaranteeSubject;
            tender.AnnouncementDate = request.AnnouncementDate;
            tender.TenderStatus = request.TenderStatus;
            tender.DocumentUploadDate = request.DocumentUploadDate;
            tender.ContractDate = request.ContractDate;
            tender.CreatedAt = request.CreatedAt;
            _db.Tenders.Update(tender);
            await _db.SaveChangesAsync();
            return tender.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var tender = await _db.Tenders.FirstOrDefaultAsync(x => x.Id == id);
            if (tender != null)
            {
                _db.Tenders.Remove(tender);
                await _db.SaveChangesAsync();
            }
        }
    }
}
