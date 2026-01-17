using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class ShipmentOrderModel
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public decimal Quantity { get; set; }
        public string Warehouse { get; set; }
        public decimal AvailableStock { get; set; }
    }
}
