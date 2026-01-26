using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Firms;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.GuaranteeCommissions
{
    public class GuaranteeCommissionWriteService
    {
        private readonly INetFlowDbContext _db;

        public GuaranteeCommissionWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateGuaranteeCommissionRequest request)
        {
            var commission = new GuaranteeCommissionEntity
            {
                Note = request.Note,
                BankReferenceNo = request.BankReferenceNo,
                CommissionAmount = request.CommissionAmount,
                CommissionEndDate = request.CommissionEndDate,
                CommissionRate = request.CommissionRate,
                CommissionStartDate = request.CommissionStartDate,
                PaymentStatus = request.PaymentStatus,
                CreatedBy = request.CreatedBy,
                CreatedAt = request.CreatedAt,
                Currency = request.Currency,
                GuaranteeId = request.GuaranteeId,
                PaymentDate = request.PaymentDate,
            };

            _db.GuaranteeCommissions.Add(commission);
            await _db.SaveChangesAsync();

            return commission.Id;
        }
        public async Task<int> EditAsync(EditGuaranteeCommissionRequest request)
        {
            var commission = await _db.GuaranteeCommissions.FirstAsync(x => x.Id == request.Id);
            commission.Id = request.Id;
            commission.Note = request.Note;
            commission.BankReferenceNo = request.BankReferenceNo;
            commission.CommissionAmount = request.CommissionAmount;
            commission.CommissionEndDate = request.CommissionEndDate;
            commission.CommissionRate = request.CommissionRate;
            commission.CommissionStartDate = request.CommissionStartDate;
            commission.PaymentStatus = request.PaymentStatus;
            commission.CreatedBy = request.CreatedBy;
            commission.CreatedAt = request.CreatedAt;
            commission.Currency = request.Currency;
            commission.GuaranteeId = request.GuaranteeId;
            commission.PaymentDate = request.PaymentDate;
            _db.GuaranteeCommissions.Update(commission);
            await _db.SaveChangesAsync();
            return commission.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var commission = await _db.GuaranteeCommissions.FirstOrDefaultAsync(x => x.Id == id);
            if (commission != null)
            {
                _db.GuaranteeCommissions.Remove(commission);
                await _db.SaveChangesAsync();
            }
        }
    }
}
