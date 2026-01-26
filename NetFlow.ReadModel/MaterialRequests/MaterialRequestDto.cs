using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.ReadModel.Requests
{
    public class MaterialRequestDto
    {
        public int Id { get; set; }
        public string RequestNo { get; set; } = null!;
        public int FirmId { get; set; }
        public int RequestedByUserId { get; set; }
        public string? RequestedDepartment { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime? RequiredDate { get; set; }
        public string RequestType { get; set; } = null!;     // Production / Maintenance / Office
        public string Priority { get; set; } = "Normal";     // Low / Normal / Urgent
        public string Status { get; set; } = "Open";          // Open / PendingApproval / Approved / Rejected / Fulfilled / Closed
        public string? Description { get; set; }
        public int? ApprovedByUserId { get; set; }
        public int? AssignedToUserId { get; set; }
        [NotMapped]
        public string? AssignedToUser { get; set; }
        [NotMapped]
        public string? RequestedByUser { get; set; }
        [NotMapped]
        public string? ApprovedByUser { get; set; }
        public string? AssignedDepartment { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? RejectionReason { get; set; }
        public string? SourceReference { get; set; }          // WorkOrder / Project / Tender
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public int CreateBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
