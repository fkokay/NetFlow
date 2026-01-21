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

        public int CompanyId { get; set; }

        public int RequestedByUserId { get; set; }
        public string? RequestedDepartment { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime? RequiredDate { get; set; }

        public string RequestType { get; set; } = null!;     // Production / Maintenance / Office
        public MaterialRequestPriority Priority { get; set; } = MaterialRequestPriority.Normal;     // Low / Normal / Urgent

        public MaterialRequestStatus Status { get; set; } = MaterialRequestStatus.Open;          // Open / PendingApproval / Approved / Rejected / Fulfilled / Closed

        public string? Description { get; set; }

        public int? ApprovedByUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? RejectionReason { get; set; }

        public FulfillmentType? FulfillmentType { get; set; }          // FromStock / Purchase / Transfer
        public string? SourceReference { get; set; }          // WorkOrder / Project / Tender

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        // Navigation properties
        public ICollection<MaterialRequestItemEntity> MaterialRequestItems { get; set; } = new List<MaterialRequestItemEntity>();
        public ICollection<MaterialRequestHistoryEntity> MaterialRequestHistories { get; set; } = new List<MaterialRequestHistoryEntity>();
    }
}
