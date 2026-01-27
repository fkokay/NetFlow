using DevExpress.XtraPrinting.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class MaterialRequestModel
    {
        public int Id { get; set; }
        public string RequestNo { get; set; } = null!;
        public int FirmId { get; set; }
        public int RequestedByUserId { get; set; }
        public string? RequestedDepartment { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime RequiredDate { get; set; } = DateTime.UtcNow.AddDays(7);
        public MaterialRequestType RequestType { get; set; } = MaterialRequestType.Production;
        public MaterialRequestPriority Priority { get; set; } = MaterialRequestPriority.Normal;
        public MaterialRequestStatus Status { get; set; } = MaterialRequestStatus.Draft;
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
        public MaterialRequestSourceType SourceType { get; set; }     
    }
}
