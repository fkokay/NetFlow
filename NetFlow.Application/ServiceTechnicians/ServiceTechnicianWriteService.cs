using NetFlow.Application.Common.Interfaces;
using NetFlow.Domain.Entities;

namespace NetFlow.Application.ServiceTechnicians
{
    public class ServiceTechnicianWriteService
    {
        private readonly INetFlowDbContext _db;
        public ServiceTechnicianWriteService(INetFlowDbContext db)
        {
            _db = db;
        }
        public async Task<int> CreateAsync(int userId, CreateServiceTechnicianRequest request)
        {
            var serviceTechnician = new ServiceTechnicianEntity
            {
                ServiceFormId = request.ServiceFormId,
                AssignedPersonnelId = request.AssignedPersonnelId,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow,
                AssignedAt = DateTime.UtcNow,
            };
            
            await _db.ServiceTechnicians.AddAsync(serviceTechnician);
            await _db.SaveChangesAsync();
            return serviceTechnician.Id;
        }
    }
}
