using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Firms;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Guarantees
{
    public class GuaranteeWriteService
    {
        private readonly INetFlowDbContext _db;

        public GuaranteeWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> EditAsync(EditGuaranteeRequest request)
        {
            var guarantee = await _db.Guarantees.FirstOrDefaultAsync(x => x.Id == request.Id);
            guarantee.FirmId = request.FirmId;
            guarantee.Subject=request.Subject;
            guarantee.GuaranteeType = request.GuaranteeType;
            guarantee.GuaranteeForm = request.GuaranteeForm;
            guarantee.GuaranteeNumber = request.GuaranteeNumber;
            guarantee.Currency = request.Currency;
            guarantee.CommissionRate = request.CommissionRate;
            guarantee.CommissionPeriodId = request.CommissionPeriodId;
            guarantee.GuaranteeDate = request.GuaranteeDate;
            guarantee.ExpiryDate = request.ExpiryDate;
            guarantee.BankCode = request.BankCode;
            guarantee.Subject = request.Subject;
            guarantee.BankBranchCode = request.BankBranchCode;
            guarantee.PublicAuthorityCode = request.PublicAuthorityCode;
            guarantee.ExpenseAccountCode = request.ExpenseAccountCode;  
            guarantee.TakasbankReferenceNo = request.TakasbankReferenceNo;
            guarantee.CreatedAt = request.CreatedAt;
            _db.Guarantees.Update(guarantee);
            await _db.SaveChangesAsync();
            return guarantee.Id;
        }
    }
}

