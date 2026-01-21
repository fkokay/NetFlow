using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class MaterialRequestModel
    {
        public int Id { get; set; }
        public string RequestNo { get; set; } = null!;
        public int CompanyId { get; set; }
        public int RequestedByUserId { get; set; }
        public string? RequestedDepartment { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime? RequiredDate { get; set; }
        public string RequestType { get; set; } = null!;     // Production / Maintenance / Office
        public string Priority { get; set; } = "Normal";     // Low / Normal / Urgent
        public string Status { get; set; } = "Open";          // Open / PendingApproval / Approved / Rejected / Fulfilled / Closed
        public string? Description { get; set; }
        public int? ApprovedByUserId { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? RejectionReason { get; set; }
        public string? FulfillmentType { get; set; }          // FromStock / Purchase / Transfer
        public string? SourceReference { get; set; }          // WorkOrder / Project / Tender
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
