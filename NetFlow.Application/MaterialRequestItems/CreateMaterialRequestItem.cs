using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.MaterialRequestItems
{
    public class CreateMaterialRequestItem
    {
        public int MaterialRequestId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string? ItemName { get; set; }
        public decimal RequestedQuantity { get; set; }
        public decimal FulfilledQuantity { get; set; }
        public string Unit { get; set; } = null!;       
        public string? WarehouseCode { get; set; }
        public string? AlternateItemCode { get; set; }
        public string Status { get; set; } = "Pending";     
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? FulfillmentType { get; set; } = "FromStock";
    }
}
