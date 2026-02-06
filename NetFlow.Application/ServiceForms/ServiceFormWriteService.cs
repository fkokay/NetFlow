using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Personnels;
using NetFlow.Application.ServiceFormHistories;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using NetFlow.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text;

namespace NetFlow.Application.ServiceForms
{
    public class ServiceFormWriteService
    {

        private readonly INetFlowDbContext _db;
        private readonly ServiceFormHistoryWriteService _historyWrite;
        public ServiceFormWriteService(INetFlowDbContext db, ServiceFormHistoryWriteService historyWrite)
        {
            _db = db;
            _historyWrite = historyWrite;
        }

        public async Task<int> CreateAsync(int userId, CreateServiceFormRequest request)
        {
            var service = new ServiceFormEntity
            {
                ServiceFormNo = request.ServiceFormNo,
                ServiceType = request.ServiceType,
                ServiceStatus = request.ServiceStatus,
                CustomerCode = request.CustomerCode,
                CustomerName = request.CustomerName,
                TenderCode = request.TenderCode,
                TenderName = request.TenderName,
                SubCustomerCode = request.SubCustomerCode,
                SubCustomerName = request.SubCustomerName,
                ProblemDescription = request.ProblemDescription,
                ServiceDescription = request.ServiceDescription,
                AssignedPersonnelId = request.AssignedPersonnelId,
                AssignedAt = request.AssignedAt,
                CreatedBy = userId,
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

            if (service.ServiceStatus != request.ServiceStatus)
            {
                await _historyWrite.CreateAsync(request.CreatedBy, new CreateServiceFormHistoryRequest
                {
                    ServiceFormId = service.Id,
                    ActionType = ServiceActionType.StatusChanged,
                    OldStatus = service.ServiceStatus,
                    NewStatus = service.ServiceStatus,
                    Description = $"Durum değişti: {service.ServiceStatus} → {request.ServiceStatus}",
                    Source = "web",
                });

                service.ServiceStatus = request.ServiceStatus;
                if (request.ServiceStatus == ServiceStatus.Closed)
                    service.ClosedAt = DateTime.UtcNow;
            }

            if (service.ServiceType != request.ServiceType)
            {
                await _historyWrite.CreateAsync(request.CreatedBy, new CreateServiceFormHistoryRequest
                {
                    ServiceFormId = service.Id,
                    ActionType = ServiceActionType.StatusChanged,
                    OldStatus = service.ServiceStatus,
                    NewStatus = service.ServiceStatus,
                    Description = $"Durum değişti: {service.ServiceType} → {request.ServiceType}",
                    Source = "web",
                });

                service.ServiceStatus = request.ServiceStatus;
                if (request.ServiceStatus == ServiceStatus.Closed)
                    service.ClosedAt = DateTime.UtcNow;
            }
            if (service.AssignedPersonnelId != request.AssignedPersonnelId)
            {
                await _historyWrite.CreateAsync(request.CreatedBy, new CreateServiceFormHistoryRequest
                {
                    ServiceFormId = service.Id,
                    ActionType = ServiceActionType.PersonnelAssigned,
                    OldPersonnelId = service.AssignedPersonnelId,
                    NewPersonnelId = request.AssignedPersonnelId,
                    Description = "Personel ataması değiştirildi",
                    Source = "web",
                });

                service.AssignedPersonnelId = request.AssignedPersonnelId;
                service.AssignedAt = DateTime.UtcNow;
            }
            bool otherChanged =
                service.ProblemDescription != request.ProblemDescription ||
                service.ServiceDescription != request.ServiceDescription ||
                service.LaborCost != request.LaborCost ||
                service.MaterialCost != request.MaterialCost ||
                service.Notes != request.Notes;

            if (otherChanged)
            {
                await _historyWrite.CreateAsync(request.CreatedBy, new CreateServiceFormHistoryRequest
                {
                    ServiceFormId = service.Id,
                    ActionType = ServiceActionType.Updated,
                    Description = "Servis bilgileri güncellendi",
                    Source = "web",
                });
            }



            service.ServiceFormNo = request.ServiceFormNo;
            service.ServiceType = request.ServiceType;
            service.ServiceStatus = request.ServiceStatus;
            service.CustomerCode = request.CustomerCode;
            service.CustomerName = request.CustomerName;
            service.TenderCode = request.TenderCode;
            service.TenderName = request.TenderName;
            service.SubCustomerCode = request.SubCustomerCode;
            service.SubCustomerName = request.SubCustomerName;
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

        public async Task<int> EditServiceFormTechnician(EditServiceFormTechnicianVRequest request)
        {
            var service = await _db.ServiceForms.FirstAsync(x => x.Id == request.ServiceFormId);

            if (service.IsTechnicianAssigned != request.IsTechnicianAssigned)
            {
                await _historyWrite.CreateAsync(request.CreatedBy, new CreateServiceFormHistoryRequest
                {
                    ServiceFormId = service.Id,
                    ActionType = ServiceActionType.StatusChanged,
                    OldStatus = service.ServiceStatus,
                    NewStatus = service.ServiceStatus,
                    Description = "Teknisyen Durumu Değişti: Teknisyen Atandı",
                    Source = "web",
                });
            }

            service.IsTechnicianAssigned = request.IsTechnicianAssigned;
            await _db.SaveChangesAsync();
            return service.Id;
        }
    }
}
