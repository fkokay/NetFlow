using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Entities
{
    public class MaterialRequestItemEntity
    {
        public int Id { get; set; }
        public int MaterialRequestId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string? ItemName { get; set; }
        public decimal RequestedQuantity { get; set; }
        public decimal FulfilledQuantity { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; } = null!;
        public string? WarehouseCode { get; set; }
        public string? AlternateItemCode { get; set; }
        public bool IsAlternateUsed { get; set; }
        public MaterialRequestItemStatus Status { get; set; } = MaterialRequestItemStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public MaterialRequestItemFulfillmentType FulfillmentType { get; set; } = MaterialRequestItemFulfillmentType.Undefined;

        // Navigation
        public MaterialRequestEntity MaterialRequest { get; set; } = null!;
    }
}
