using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.MaterialRequests
{
    public class FulfillmentRequest
    {
        public int Id { get; set; }
        public List<FulfillmentRequestItem> Items { get; set; } = new List<FulfillmentRequestItem>();
    }

    public class FulfillmentRequestItem
    {
        public int ItemId { get; set; }
        public MaterialRequestItemFulfillmentType FulfillmentType { get; set; } = MaterialRequestItemFulfillmentType.Undefined;
        public decimal RequestedQuantity { get; set; }
        public decimal FulfilledQuantity { get; set; }
        public string? Currency { get; set; }
        public string? PurchaseCustomerCode { get; set; }
    }
}
