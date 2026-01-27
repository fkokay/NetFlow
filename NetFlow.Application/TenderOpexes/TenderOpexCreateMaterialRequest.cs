using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderOpexes
{
    public class TenderOpexCreateMaterialRequest
    {
        public int TenderId { get; set; }   
        public int TenderOpexId { get; set; }
        public string RequestType { get; set; } = null!;
        public DateTime? RequiredDate { get; set; }
        public string Priority { get; set; } = "Normal";
        public string? RequestedDepartment { get; set; }
        public string? Description { get; set; }
        public string? SourceReference { get; set; }

        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public decimal RequestedQuantity { get; set; }
        public decimal FulfilledQuantity { get; set; }
        public string Unit { get; set; } = null!;
        public string WarehouseCode { get; set; } = null!;
        public string? AlternateItemCode { get; set; }
        public string Status { get; set; } = "Open";
        public string FulfillmentType { get; set; } = "Purchase";
    }
}
