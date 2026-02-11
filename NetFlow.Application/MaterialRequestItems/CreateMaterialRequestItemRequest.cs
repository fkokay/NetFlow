using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace NetFlow.Application.MaterialRequestItems
{
    public class CreateMaterialRequestItemRequest
    {
        public int MaterialRequestId { get; set; }
        public string StockCode { get; set; } = null!;
        public decimal RequestedQuantity { get; set; }
        public decimal FulfilledQuantity { get; set; }
        public decimal Price{ get; set; }
        public string Unit { get; set; } = null!;       
        public string? WarehouseCode { get; set; }
        public string? AlternateItemCode { get; set; }
        public string? PurchaseCustomerCode { get; set; }
        public string? Currency { get; set; }
        public MaterialRequestItemStatus Status { get; set; } = MaterialRequestItemStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public MaterialRequestItemFulfillmentType FulfillmentType { get; set; } = MaterialRequestItemFulfillmentType.Undefined;
    }
}
