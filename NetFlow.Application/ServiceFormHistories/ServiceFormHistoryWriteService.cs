using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.ServiceFormDetails;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.ServiceFormHistories
{
    public class ServiceFormHistoryWriteService
    {
        private readonly INetFlowDbContext _db;
        public ServiceFormHistoryWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(int userId, CreateServiceFormHistoryRequest request)
        {
            var serviceHistory = new ServiceFormHistoryEntity
            {
                ServiceFormId = request.ServiceFormId,
                ActionType = request.ActionType,
                OldStatus = request.OldStatus,
                NewStatus = request.NewStatus,
                OldPersonnelId = request.OldPersonnelId,
                NewPersonnelId = request.NewPersonnelId,
                Description = request.Description,
                ActionBy = userId,
                ActionAt = DateTime.UtcNow,
                IpAddress = request.IpAddress,
                Source = request.Source
            };
            await _db.ServiceFormHistories.AddAsync(serviceHistory);
            await _db.SaveChangesAsync();
            return serviceHistory.Id;
        }
    }
}
