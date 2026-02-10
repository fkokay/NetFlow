using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderCapexes
{
    public class TenderCapexCreateMaterialRequest
    {
        public int TenderId { get; set; }
        public int TenderCapexId { get; set; }
        public MaterialRequestType RequestType { get; set; } = MaterialRequestType.Tender;
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
