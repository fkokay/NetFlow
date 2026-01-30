using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class FulfillmentRequestModel
    {
        public int Id { get; set; }
        public List<FulfillmentRequestItemModel> Items { get; set; } = new List<FulfillmentRequestItemModel>();
    }

    public class FulfillmentRequestItemModel
    {
        public int ItemId { get; set; }
        public MaterialRequestItemFulfillmentType FulfillmentType { get; set; } = MaterialRequestItemFulfillmentType.Undefined;
        public decimal RequestedQuantity { get; set; }
        public decimal FulfilledQuantity { get; set; }
    }
}
