using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Utilities;
using NetFlow.Domain.Entities;

namespace NetFlow.Application.Guarantees
{
    public class GuaranteeWriteService
    {
        private readonly INetFlowDbContext _db;

        public GuaranteeWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateGuaranteeRequest request)
        {
            var guarantee = new GuaranteeEntity
            {
                FirmId = request.FirmId,
                Subject = request.Subject,
                GuaranteeType = request.GuaranteeType,
                GuaranteeForm = request.GuaranteeForm,
                GuaranteeNumber = request.GuaranteeNumber,
                GuaranteeAmount = request.GuaranteeAmount,
                Currency = request.Currency,
                CommissionRate = request.CommissionRate,
                CommissionAmount = request.CommissionAmount,
                CommissionPeriodId = 1,
                GuaranteeDate = request.GuaranteeDate,
                ExpiryDate = request.ExpiryDate,
                BankCode = request.BankCode,
                BankBranchCode = request.BankBranchCode,
                PublicAuthorityCode = request.PublicAuthorityCode,
                ExpenseAccountCode = request.ExpenseAccountCode,
                TakasbankReferenceNo = request.TakasbankReferenceNo,
                CreatedAt = DateTime.Now
            };

            guarantee.CommissionPeriod = await _db.GuaranteeCommissionPeriods
                .FirstOrDefaultAsync(x => x.Id == guarantee.CommissionPeriodId);

            _db.Guarantees.Add(guarantee);
            await _db.SaveChangesAsync();

            await CreateGuaranteeCommissionsAsync(guarantee);

            return guarantee.Id;
        }
        public async Task<int> EditAsync(EditGuaranteeRequest request)
        {
            var guarantee = await _db.Guarantees
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (guarantee == null)
                throw new Exception("Teminat bulunamadı");


            bool commissionChanged =
                guarantee.CommissionRate != request.CommissionRate ||
                guarantee.GuaranteeAmount != request.GuaranteeAmount ||
                guarantee.CommissionPeriodId != request.CommissionPeriodId ||
                guarantee.GuaranteeDate != request.GuaranteeDate ||
                guarantee.ExpiryDate != request.ExpiryDate;

            guarantee.FirmId = request.FirmId;
            guarantee.Subject = request.Subject;
            guarantee.GuaranteeType = request.GuaranteeType;
            guarantee.GuaranteeForm = request.GuaranteeForm;
            guarantee.GuaranteeNumber = request.GuaranteeNumber;
            guarantee.Currency = request.Currency;
            guarantee.CommissionRate = request.CommissionRate;
            guarantee.CommissionPeriodId = request.CommissionPeriodId;
            guarantee.GuaranteeDate = request.GuaranteeDate;
            guarantee.GuaranteeAmount = request.GuaranteeAmount;
            guarantee.CommissionAmount = request.CommissionAmount;
            guarantee.ExpiryDate = request.ExpiryDate;
            guarantee.BankCode = request.BankCode;
            guarantee.BankBranchCode = request.BankBranchCode;
            guarantee.PublicAuthorityCode = request.PublicAuthorityCode;
            guarantee.ExpenseAccountCode = request.ExpenseAccountCode;
            guarantee.TakasbankReferenceNo = request.TakasbankReferenceNo;

            guarantee.CommissionPeriod = await _db.GuaranteeCommissionPeriods
                .FirstOrDefaultAsync(x => x.Id == guarantee.CommissionPeriodId);

            await _db.SaveChangesAsync();

            if (commissionChanged)
            {
                var oldCommissions = await _db.GuaranteeCommissions
                    .Where(x => x.GuaranteeId == guarantee.Id)
                    .ToListAsync();

                if (oldCommissions.Any())
                    _db.GuaranteeCommissions.RemoveRange(oldCommissions);

                await _db.SaveChangesAsync();

                await CreateGuaranteeCommissionsAsync(guarantee);
            }

            return guarantee.Id;
        }
        public async Task<int> EditGuaranteeExtensionAsync(EditGuaranteeExtensionRequest request)
        {
            var guarantee = await _db.Guarantees
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (guarantee == null)
                throw new Exception("Teminat bulunamadı");

          
            var oldExpiryDate = guarantee.ExpiryDate;

            bool commissionChanged = oldExpiryDate != request.ExpiryDate;

            guarantee.ExpiryDate = request.ExpiryDate;

            guarantee.CommissionPeriod = await _db.GuaranteeCommissionPeriods
                .FirstOrDefaultAsync(x => x.Id == guarantee.CommissionPeriodId);

            await _db.SaveChangesAsync();

        
            if (commissionChanged && request.ExpiryDate > oldExpiryDate)
            {
                var extraMonthCount = DateUtils.GetMonthDifference(
                    oldExpiryDate,
                    request.ExpiryDate);

                int loopCount = extraMonthCount / guarantee.CommissionPeriod.Period;

                for (int i = 0; i < loopCount; i++)
                {
                    var commission = new GuaranteeCommissionEntity
                    {
                        GuaranteeId = guarantee.Id,
                        CommissionStartDate = oldExpiryDate.AddMonths(i * guarantee.CommissionPeriod.Period),
                        CommissionEndDate = oldExpiryDate.AddMonths((i + 1) * guarantee.CommissionPeriod.Period),
                        CommissionRate = guarantee.CommissionRate,
                        CommissionAmount = Math.Round(
                            guarantee.GuaranteeAmount *
                            guarantee.CommissionRate *
                            (guarantee.CommissionPeriod.Period / 12m),
                            2,
                            MidpointRounding.AwayFromZero),
                        Currency = guarantee.Currency,
                        PaymentStatus = "Beklemede",
                        CreatedAt = DateTime.Now,
                        CreatedBy = "System"
                    };

                    _db.GuaranteeCommissions.Add(commission);
                }

                await _db.SaveChangesAsync();
            }

            return guarantee.Id;
        }


        public async Task DeleteAsync(int id)
        {
            bool isUsedInTender = await _db.Tenders.AnyAsync(t =>
                t.TemporaryGuaranteeRateId == id ||
                t.FinalGuaranteeRateId == id
            );

            if (isUsedInTender)
                throw new InvalidOperationException(
                    "Bu teminat bir veya daha fazla ihalede kullanıldığı için silinemez."
                );

            var guarantee = await _db.Guarantees.FirstOrDefaultAsync(x => x.Id == id);
            if (guarantee == null)
                return;

            var commissions = await _db.GuaranteeCommissions
                .Where(x => x.GuaranteeId == id)
                .ToListAsync();

            if (commissions.Any())
                _db.GuaranteeCommissions.RemoveRange(commissions);

            _db.Guarantees.Remove(guarantee);
            await _db.SaveChangesAsync();
        }
        
        private async Task CreateGuaranteeCommissionsAsync(GuaranteeEntity guarantee)
        {
            if (guarantee == null)
                throw new ArgumentNullException(nameof(guarantee));

            var periodCount = DateUtils.GetMonthDifference(
                guarantee.GuaranteeDate,
                guarantee.ExpiryDate);

            int loopCount = periodCount / guarantee.CommissionPeriod.Period;

            for (int i = 0; i < loopCount; i++)
            {
                var commission = new GuaranteeCommissionEntity
                {
                    GuaranteeId = guarantee.Id,
                    CommissionStartDate = guarantee.GuaranteeDate.AddMonths(i * guarantee.CommissionPeriod.Period),
                    CommissionEndDate = guarantee.GuaranteeDate.AddMonths((i + 1) * guarantee.CommissionPeriod.Period),
                    CommissionRate = guarantee.CommissionRate,
                    CommissionAmount = Math.Round(
                        guarantee.GuaranteeAmount *
                        guarantee.CommissionRate *
                        (guarantee.CommissionPeriod.Period / 12m),
                        2,
                        MidpointRounding.AwayFromZero),
                    Currency = guarantee.Currency,
                    PaymentStatus = "Beklemede",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System"
                };

                _db.GuaranteeCommissions.Add(commission);
            }

            await _db.SaveChangesAsync();
        }

    }

}

