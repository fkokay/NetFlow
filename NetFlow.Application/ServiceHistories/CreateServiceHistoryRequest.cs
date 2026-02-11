using NetFlow.Domain.Enums;

namespace NetFlow.Application.ServiceHistories
{
    public class CreateServiceHistoryRequest
    {
        public int ServiceFormId { get; set; }
        public ServiceActionType ActionType { get; set; }= ServiceActionType.Undefined;
        public ServiceStatus? OldStatus { get; set; }
        public ServiceStatus? NewStatus { get; set; }
        public int? OldPersonnelId { get; set; }
        public int? NewPersonnelId { get; set; }
        public string? Description { get; set; }
        public int ActionBy { get; set; }
        public DateTime ActionAt { get; set; }
        public string? IpAddress { get; set; }
        public string? Source { get; set; }
    }
}
