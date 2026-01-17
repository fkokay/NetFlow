using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NetFlow.Blazor.Shared.Models.Netsis
{
    public class OrderModel
    {
        public short BranchCode { get; set; }
        public string OrderType { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public short OrderStatus { get; set; }
        public string? Description { get; set; }
        public double NetTotal { get; set; }
    }
}
