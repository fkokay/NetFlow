using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Firms;
using NetFlow.Application.MaterialRequests;
using NetFlow.Application.Utilities;
using NetFlow.Domain.Entities;
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
            guarantee.CommissionPeriod = await _db.GuaranteeCommissionPeriods.FirstOrDefaultAsync(x => x.Id == guarantee.CommissionPeriodId);

            _db.Guarantees.Update(guarantee);
            await _db.SaveChangesAsync();

            await CreateGuaranteeCommissionsAsync(guarantee);
            return guarantee.Id;
        }


        public async Task<int> CreateAsync(CreateGuaranteeRequest request)
        {

            var guarantee = new GuaranteeEntity();
            guarantee.FirmId = request.FirmId;
            guarantee.Subject = request.Subject;
            guarantee.GuaranteeType = request.GuaranteeType;
            guarantee.GuaranteeForm = request.GuaranteeForm;
            guarantee.GuaranteeNumber = request.GuaranteeNumber;
            guarantee.GuaranteeAmount = request.GuaranteeAmount;
            guarantee.Currency = request.Currency;
            guarantee.CommissionRate = request.CommissionRate;
            guarantee.CommissionAmount = request.CommissionAmount;
            guarantee.CommissionPeriodId = 1;
            guarantee.GuaranteeDate = request.GuaranteeDate;
            guarantee.ExpiryDate = request.ExpiryDate;
            guarantee.BankCode = request.BankCode;
            guarantee.BankBranchCode = request.BankBranchCode;
            guarantee.PublicAuthorityCode = request.PublicAuthorityCode;
            guarantee.ExpenseAccountCode = request.ExpenseAccountCode;
            guarantee.TakasbankReferenceNo = request.TakasbankReferenceNo;
            guarantee.CreatedAt = DateTime.Now;
            guarantee.CommissionPeriod = await _db.GuaranteeCommissionPeriods.FirstOrDefaultAsync(x => x.Id == guarantee.CommissionPeriodId);
            _db.Guarantees.Add(guarantee);
            await _db.SaveChangesAsync();


            await CreateGuaranteeCommissionsAsync(guarantee);
            return guarantee.Id;
        }


        private async Task CreateGuaranteeCommissionsAsync(GuaranteeEntity guarantee)
        {
            if (guarantee == null)
                throw new ArgumentNullException(nameof(guarantee));


            var period = DateUtils.GetMonthDifference(
                guarantee.GuaranteeDate,
                guarantee.ExpiryDate);

          
            for (int i = 0; i < period / guarantee.CommissionPeriod.Period; i++)
            {
                GuaranteeCommissionEntity commission = new GuaranteeCommissionEntity();
                commission.GuaranteeId = guarantee.Id;
                commission.CommissionStartDate = guarantee.GuaranteeDate.AddMonths(i * guarantee.CommissionPeriod.Period);
                commission.CommissionEndDate = guarantee.GuaranteeDate.AddMonths((i + 1) * guarantee.CommissionPeriod.Period);
                commission.CommissionRate = guarantee.CommissionRate;
                commission.CommissionAmount = Math.Round(guarantee.GuaranteeAmount * guarantee.CommissionRate * (guarantee.CommissionPeriod.Period / 12m), 2, MidpointRounding.AwayFromZero);
                commission.Currency = guarantee.Currency;
                commission.PaymentStatus = "Beklemede";
                commission.CreatedAt = DateTime.Now;
                commission.CreatedBy = "System";
                _db.GuaranteeCommissions.Add(commission);
                await _db.SaveChangesAsync();
            }
        }
    }
}

