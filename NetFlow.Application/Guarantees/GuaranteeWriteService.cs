using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Utilities;
using NetFlow.Domain.Entities;

namespace NetFlow.Application.Guarantees
{
    public class GuaranteeWriteService(INetFlowDbContext db)
    {
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

            guarantee.CommissionPeriod = await db.GuaranteeCommissionPeriods.FirstAsync(x => x.Id == guarantee.CommissionPeriodId);

            db.Guarantees.Add(guarantee);
            await db.SaveChangesAsync();

            await CreateGuaranteeCommissionsAsync(guarantee);

            return guarantee.Id;
        }
        public async Task<int> EditAsync(EditGuaranteeRequest request)
        {
            var guarantee = await db.Guarantees
                .FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new Exception("Teminat bulunamadı");
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

            guarantee.CommissionPeriod = await db.GuaranteeCommissionPeriods.FirstAsync(x => x.Id == guarantee.CommissionPeriodId);

            await db.SaveChangesAsync();

            if (commissionChanged)
            {
                var oldCommissions = await db.GuaranteeCommissions
                    .Where(x => x.GuaranteeId == guarantee.Id)
                    .ToListAsync();

                if (oldCommissions.Count != 0)
                    db.GuaranteeCommissions.RemoveRange(oldCommissions);

                await db.SaveChangesAsync();

                await CreateGuaranteeCommissionsAsync(guarantee);
            }

            return guarantee.Id;
        }
        public async Task<int> EditGuaranteeExtensionAsync(EditGuaranteeExtensionRequest request)
        {
            var guarantee = await db.Guarantees
                .FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new Exception("Teminat bulunamadı");
            var oldExpiryDate = guarantee.ExpiryDate;

            bool commissionChanged = oldExpiryDate != request.ExpiryDate;

            guarantee.ExpiryDate = request.ExpiryDate;

            guarantee.CommissionPeriod = await db.GuaranteeCommissionPeriods.FirstAsync(x => x.Id == guarantee.CommissionPeriodId);

            await db.SaveChangesAsync();


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

                    db.GuaranteeCommissions.Add(commission);
                }

                await db.SaveChangesAsync();
            }

            return guarantee.Id;
        }

        public async Task<int> ReturnRequestAsync(ReturnRequest request)
        {
            var guarantee = await db.Guarantees.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new Exception("Teminat bulunamadı");
            guarantee.ReturnDate = request.ReturnDate;
            guarantee.IsRefunded=true;
            await db.SaveChangesAsync();
            return guarantee.Id;
        }
        public async Task DeleteAsync(int id)
        {
            bool isUsedInTender = await db.Tenders.AnyAsync(t =>
                t.TemporaryGuaranteeRateId == id ||
                t.FinalGuaranteeRateId == id
            );

            if (isUsedInTender)
                throw new InvalidOperationException(
                    "Bu teminat bir veya daha fazla ihalede kullanıldığı için silinemez."
                );

            var guarantee = await db.Guarantees.FirstOrDefaultAsync(x => x.Id == id);
            if (guarantee == null)
                return;

            var commissions = await db.GuaranteeCommissions
                .Where(x => x.GuaranteeId == id)
                .ToListAsync();

            if (commissions.Count != 0)
                db.GuaranteeCommissions.RemoveRange(commissions);

            db.Guarantees.Remove(guarantee);
            await db.SaveChangesAsync();
        }

        private async Task CreateGuaranteeCommissionsAsync(GuaranteeEntity guarantee)
        {
            ArgumentNullException.ThrowIfNull(guarantee);
            ArgumentNullException.ThrowIfNull(guarantee.CommissionPeriod);

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

                db.GuaranteeCommissions.Add(commission);
            }

            await db.SaveChangesAsync();
        }

    }

}

