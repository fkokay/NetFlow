using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Orders
{
    public class Order
    {
        public short BranchCode { get; set; }
        public short OrderType { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public short OrderStatus { get; set; }
        public string? Description { get; set; }
        public decimal NetTotal { get; set; }
    }
}
