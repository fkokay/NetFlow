using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.MaterialRequestItems
{
    public class MaterialRequestItemDto
    {
        public int Id { get; set; }
        public int MaterialRequestId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string? ItemName { get; set; }
        public decimal RequestedQuantity { get; set; }
        public decimal FulfilledQuantity { get; set; }
        public string Unit { get; set; } = null!;              // pcs / kg / meter
        public string? WarehouseCode { get; set; }
        public string? AlternateItemCode { get; set; }
        public string? FulfillmentType { get; set; }          // FromStock / Purchase / Transfer
        public string Status { get; set; } = "Pending";        // Pending / Fulfilled / Partial / Cancelled
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
