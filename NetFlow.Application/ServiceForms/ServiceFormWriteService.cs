using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Personnels;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Application.ServiceForms
{
    public class ServiceFormWriteService
    {

        private readonly INetFlowDbContext _db;
        public ServiceFormWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(int userId,CreateServiceFormRequest request)
        {
            var service = new ServiceFormEntity
            {
                ServiceFormNo = request.ServiceFormNo,
                ServiceType = request.ServiceType,
                ServiceStatus = request.ServiceStatus,
                CustomerCode = request.CustomerCode,
                CustomerName = request.CustomerName,
                ProblemDescription = request.ProblemDescription,
                ServiceDescription = request.ServiceDescription,
                AssignedPersonnelId = request.AssignedPersonnelId,
                AssignedAt = request.AssignedAt,
                CreatedBy= userId,
                ServiceStartDate = request.ServiceStartDate,
                ServiceEndDate = request.ServiceEndDate,
                IsOnSite = request.IsOnSite,
                IsWarranty = request.IsWarranty,
                LaborCost = request.LaborCost,
                MaterialCost = request.MaterialCost,
                Notes = request.Notes,
                CreatedAt = request.CreatedAt,
            };
            _db.ServiceForms.Add(service);
            await _db.SaveChangesAsync();
            return service.Id;
        }
        public async Task<int> EditAsync(EditServiceFormRequest request)
        {
            var service = await _db.ServiceForms.FirstAsync(x => x.Id == request.Id);
            service.ServiceFormNo = request.ServiceFormNo;
            service.ServiceType = request.ServiceType;
            service.ServiceStatus = request.ServiceStatus;
            service.CustomerCode = request.CustomerCode;
            service.CustomerName = request.CustomerName;
            service.CreatedBy = request.CreatedBy;
            service.ProblemDescription = request.ProblemDescription;
            service.ServiceDescription = request.ServiceDescription;
            service.AssignedPersonnelId = request.AssignedPersonnelId;
            service.AssignedAt = request.AssignedAt;
            service.ServiceStartDate = request.ServiceStartDate;
            service.ServiceEndDate = request.ServiceEndDate;
            service.IsOnSite = request.IsOnSite;
            service.IsWarranty = request.IsWarranty;
            service.LaborCost = request.LaborCost;
            service.MaterialCost = request.MaterialCost;
            service.Notes = request.Notes;
            service.CreatedAt = request.CreatedAt;
            service.UpdatedAt = request.UpdatedAt;
            service.ClosedAt = request.ClosedAt;
            _db.ServiceForms.Update(service);
            await _db.SaveChangesAsync();
            return service.Id;
        }
    }
}
