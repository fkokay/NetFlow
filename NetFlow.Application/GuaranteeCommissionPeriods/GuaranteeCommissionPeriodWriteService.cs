using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.GuaranteeCommissions;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.GuaranteeCommissionPeriods
{
    public class GuaranteeCommissionPeriodWriteService(INetFlowDbContext db)
    {
        public async Task<int> CreateAsync(CreateGuaranteeCommissionPeriodRequest request)
        {
            var commissionPeriod = new GuaranteeCommissionPeriodEntity
            {
                PeriodName = request.PeriodName,
                Period = request.Period
            };

            db.GuaranteeCommissionPeriods.Add(commissionPeriod);
            await db.SaveChangesAsync();

            return commissionPeriod.Id;
        }
        public async Task<int> EditAsync(EditGuaranteeCommissionPeriodRequest request)
        {
            var commissionPeriod = await db.GuaranteeCommissionPeriods.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new Exception("Teminat komisyon periyodu bulunamadı");
            commissionPeriod.PeriodName = request.PeriodName;
            commissionPeriod.Period = request.Period;

            db.GuaranteeCommissionPeriods.Update(commissionPeriod);
            await db.SaveChangesAsync();
            return commissionPeriod.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var commissionPeriod = await db.GuaranteeCommissionPeriods.FirstOrDefaultAsync(x => x.Id == id);
            if (commissionPeriod != null)
            {
                db.GuaranteeCommissionPeriods.Remove(commissionPeriod);
                await db.SaveChangesAsync();
            }
        }
    }
}
