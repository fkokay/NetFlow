using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderReaktifs
{
    public class TenderReaktifCreateMaterialRequest
    {
        public int TenderId { get; set; }
        public int TenderReaktifId { get; set; }
        public MaterialRequestType RequestType { get; set; } = MaterialRequestType.Project;
        public DateTime RequiredDate { get; set; }
        public MaterialRequestPriority Priority { get; set; } = MaterialRequestPriority.Normal;
        public string? RequestedDepartment { get; set; }
        public string? Description { get; set; }
        public MaterialRequestSourceType SourceType { get; set; } = MaterialRequestSourceType.None;
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public decimal RequestedQuantity { get; set; }
        public decimal FulfilledQuantity { get; set; }
        public string Unit { get; set; } = null!;
        public string WarehouseCode { get; set; } = null!;
        public string? AlternateItemCode { get; set; }
        public MaterialRequestItemStatus Status { get; set; } = MaterialRequestItemStatus.Pending;
        public MaterialRequestItemFulfillmentType FulfillmentType { get; set; } = MaterialRequestItemFulfillmentType.Undefined;
    }
}
