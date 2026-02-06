using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.ServiceForms;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NetFlow.Application.ServiceFormDetails
{
    public class ServiceFormDetailWriteService
    {
        private readonly INetFlowDbContext _db;
        public ServiceFormDetailWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(int userId, CreateServiceFormDetailRequest request)
        {
            var nextLineNo =
                await _db.ServiceFormDetails
                    .Where(x => x.ServiceFormId == request.ServiceFormId)
                    .Select(x => (int?)x.LineNo)
                    .MaxAsync() ?? 0;

            var serviceDetail = new ServiceFormDetailEntity
            {
                ServiceFormId = request.ServiceFormId,
                DetailType = request.DetailType,
                LineNo = nextLineNo + 1,
                StockCode = request.StockCode,
                StockName = request.StockName,
                Description = request.Description,
                Quantity = request.Quantity,
                Unit = request.Unit,
                UnitPrice = request.UnitPrice,
                DiscountRate = request.DiscountRate,
                TaxRate = request.TaxRate,
                IsWarranty = request.IsWarranty,
                IsBillable = request.IsBillable,
                CreatedAt = DateTime.Now
            };

            _db.ServiceFormDetails.Add(serviceDetail);
            await _db.SaveChangesAsync(); 

            await RecalculateServiceFormTotals(request.ServiceFormId);

            return serviceDetail.Id;
        }

        public async Task<int> EditAsync(EditServiceFormDetailRequest request)
        {
            var serviceDetail =
                await _db.ServiceFormDetails.FirstAsync(x => x.Id == request.Id);

            serviceDetail.DetailType = request.DetailType;
            serviceDetail.StockCode = request.StockCode;
            serviceDetail.StockName = request.StockName;
            serviceDetail.Description = request.Description;
            serviceDetail.Quantity = request.Quantity;
            serviceDetail.Unit = request.Unit;
            serviceDetail.UnitPrice = request.UnitPrice;
            serviceDetail.DiscountRate = request.DiscountRate;
            serviceDetail.TaxRate = request.TaxRate;
            serviceDetail.IsWarranty = request.IsWarranty;
            serviceDetail.IsBillable = request.IsBillable;
            serviceDetail.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();

            await RecalculateServiceFormTotals(serviceDetail.ServiceFormId); 

            return serviceDetail.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var serviceDetail =
                await _db.ServiceFormDetails.FirstOrDefaultAsync(x => x.Id == id);

            if (serviceDetail == null) return;

            var serviceFormId = serviceDetail.ServiceFormId;

            _db.ServiceFormDetails.Remove(serviceDetail);
            await _db.SaveChangesAsync(); 

            await RecalculateServiceFormTotals(serviceFormId); 
        }
        private async Task RecalculateServiceFormTotals(int serviceFormId)
        {
            var details =
                await _db.ServiceFormDetails
                    .Where(x => x.ServiceFormId == serviceFormId && x.IsBillable)
                    .ToListAsync();

            var serviceFeeTotal =
                details
                    .Where(x => x.DetailType == ServiceDetailType.ServiceFee)
                    .Sum(x => x.TotalAmount);

            var materialTotal =
                details
                    .Where(x =>
                        x.DetailType == ServiceDetailType.Material ||
                        x.DetailType == ServiceDetailType.Expense)
                    .Sum(x => x.TotalAmount);

            var form = await _db.ServiceForms.FindAsync(serviceFormId);
            if (form == null) return;

            form.LaborCost = serviceFeeTotal;
            form.MaterialCost = materialTotal;
            form.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();
        }

    }
}
