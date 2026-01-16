using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class ShipmentOrderModel
    {
        public string OrderNo { get; }
        public string StockCode { get; }
        public decimal Quantity { get; }
        public string Warehouse { get; }
        public decimal AvailableStock { get; }
    }
}
