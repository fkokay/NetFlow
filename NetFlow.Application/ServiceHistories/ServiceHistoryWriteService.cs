using NetFlow.Application.Common.Interfaces;
using NetFlow.Domain.Entities;

namespace NetFlow.Application.ServiceHistories
{
    public class ServiceHistoryWriteService
    {
        private readonly INetFlowDbContext _db;
        public ServiceHistoryWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(int userId, CreateServiceHistoryRequest request)
        {
            var serviceHistory = new ServiceHistoryEntity
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
            await _db.ServiceHistories.AddAsync(serviceHistory);
            await _db.SaveChangesAsync();
            return serviceHistory.Id;
        }
    }
}
