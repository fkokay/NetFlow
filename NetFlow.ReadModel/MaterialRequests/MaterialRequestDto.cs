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
        public DateTime RequiredDate { get; set; } = DateTime.UtcNow;
        public int RequestType { get; set; } 
        public int Priority { get; set; }
        public int Status { get; set; }
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
        public int SourceType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
