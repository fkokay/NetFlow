using NetFlow.Domain.Enums;

namespace NetFlow.Application.MaterialRequests
{
    public class EditMaterialRequest
    {
        public int Id { get; set; }
        public int AssignedToUserId { get; set; }
        public MaterialRequestType RequestType { get; set; } = MaterialRequestType.Tender;
        public DateTime RequiredDate { get; set; }
        public MaterialRequestPriority Priority { get; set; } = MaterialRequestPriority.Normal;
        public string? RequestedDepartment { get; set; }
        public string? Description { get; set; }
        public MaterialRequestSourceType SourceType { get; set; } = MaterialRequestSourceType.None;
    }
}
