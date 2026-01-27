using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.GuaranteeCommissions;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.GuaranteeCommissionPeriods
{
    public class GuaranteeCommissionPeriodWriteService
    {
        private readonly INetFlowDbContext _db;

        public GuaranteeCommissionPeriodWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateGuaranteeCommissionPeriodRequest request)
        {
            var commissionPeriod = new GuaranteeCommissionPeriodEntity
            {
                PeriodName = request.PeriodName,
                Period = request.Period
            };

            _db.GuaranteeCommissionPeriods.Add(commissionPeriod);
            await _db.SaveChangesAsync();

            return commissionPeriod.Id;
        }
        public async Task<int> EditAsync(EditGuaranteeCommissionPeriodRequest request)
        {
            var commissionPeriod = await _db.GuaranteeCommissionPeriods.FirstOrDefaultAsync(x => x.Id == request.Id);
            commissionPeriod.PeriodName = request.PeriodName;
            commissionPeriod.Period = request.Period;
            _db.GuaranteeCommissionPeriods.Update(commissionPeriod);
            await _db.SaveChangesAsync();
            return commissionPeriod.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var commissionPeriod = await _db.GuaranteeCommissionPeriods.FirstOrDefaultAsync(x => x.Id == id);
            if (commissionPeriod != null)
            {
                _db.GuaranteeCommissionPeriods.Remove(commissionPeriod);
                await _db.SaveChangesAsync();
            }
        }
    }
}
