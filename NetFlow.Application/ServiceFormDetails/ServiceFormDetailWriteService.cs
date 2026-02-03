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
            var serviceDetail = new ServiceFormDetailEntity
            {
                ServiceFormId = request.ServiceFormId,
                LineNo = request.LineNo,
                DetailType = request.DetailType,
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
                CreatedAt = request.CreatedAt,
            };
            _db.ServiceFormDetails.Add(serviceDetail);
            await _db.SaveChangesAsync();
            return serviceDetail.Id;
        }
        public async Task<int> EditAsync(EditServiceFormDetailRequest request)
        {
            var serviceDetail = await _db.ServiceFormDetails.FirstAsync(x => x.Id == request.Id);
            serviceDetail.ServiceFormId = request.ServiceFormId;
            serviceDetail.LineNo = request.LineNo;
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
            serviceDetail.CreatedAt = request.CreatedAt;
            serviceDetail.UpdatedAt = request.UpdatedAt;
            _db.ServiceFormDetails.Update(serviceDetail);
            await _db.SaveChangesAsync();
            return serviceDetail.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var serviceDetail = await _db.ServiceFormDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (serviceDetail != null)
            {
                _db.ServiceFormDetails.Remove(serviceDetail);
                await _db.SaveChangesAsync();
            }
        }
    }
}
