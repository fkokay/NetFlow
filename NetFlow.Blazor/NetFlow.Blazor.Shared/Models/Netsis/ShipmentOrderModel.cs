using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Models.Netsis
{
    public class ShipmentOrderModel
    {
        public int Id { get; set; }
        public string OrderNo { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;    
        public string CustomerName { get; set; } = string.Empty;
        public string StockCode { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Warehouse { get; set; } = string.Empty;
        public decimal AvailableStock { get; set; }
    }
}
