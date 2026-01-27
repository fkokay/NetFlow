using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Entities
{
    public class MaterialRequestEntity
    {
        public int Id { get; set; }
        public string RequestNo { get; set; } = null!;
        public int FirmId { get; set; }
        public int RequestedByUserId { get; set; }
        public string? RequestedDepartment { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime RequiredDate { get; set; } = DateTime.UtcNow.AddDays(7);
        public MaterialRequestType RequestType { get; set; } = MaterialRequestType.Project;
        public MaterialRequestPriority Priority { get; set; } = MaterialRequestPriority.Normal; 
        public MaterialRequestStatus Status { get; set; } = MaterialRequestStatus.Open;         
        public string? Description { get; set; }
        public int? ApprovedByUserId { get; set; }
        public int? AssignedToUserId { get; set; }
        public string? AssignedDepartment { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? RejectionReason { get; set; }
        public MaterialRequestSourceType SourceType { get; set; } = MaterialRequestSourceType.None;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedByUserId { get; set; }

        public ICollection<MaterialRequestItemEntity> MaterialRequestItems { get; set; } = new List<MaterialRequestItemEntity>();
        public ICollection<MaterialRequestHistoryEntity> MaterialRequestHistories { get; set; } = new List<MaterialRequestHistoryEntity>();
    }
}
