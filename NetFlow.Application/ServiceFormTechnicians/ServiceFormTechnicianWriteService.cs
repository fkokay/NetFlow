using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.ServiceFormHistories;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.ServiceFormTechnicians
{
    public class ServiceFormTechnicianWriteService
    {
        private readonly INetFlowDbContext _db;
        public ServiceFormTechnicianWriteService(INetFlowDbContext db)
        {
            _db = db;
        }
        public async Task<int> CreateAsync(int userId, CreateServiceFormTechnicianRequest request)
        {
            var serviceTechnician = new ServiceFormTechnicianEntity
            {
                ServiceFormId = request.ServiceFormId,
                AssignedPersonnelId = request.AssignedPersonnelId,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow,
                AssignedAt = DateTime.UtcNow,
            };
            
            await _db.ServiceFormTechnicians.AddAsync(serviceTechnician);
            await _db.SaveChangesAsync();
            return serviceTechnician.Id;
        }
    }
}
