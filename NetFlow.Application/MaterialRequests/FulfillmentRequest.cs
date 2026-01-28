using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.MaterialRequests
{
    public class FulfillmentRequest
    {
        public int ItemId { get; set; }
        public MaterialRequestItemFulfillmentType FulfillmentType { get; set; } = MaterialRequestItemFulfillmentType.Undefined;
        public decimal RequestedQuantity { get; set; }
        public decimal FulfilledQuantity { get; set; }

    }
}
