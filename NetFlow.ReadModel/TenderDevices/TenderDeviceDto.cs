using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderDevices
{
    public sealed class TenderDeviceDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }

        public string SupplyType { get; set; } = string.Empty;
        public string StockCode { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;

        public string Currency { get; set; } = string.Empty;

        public decimal RentUnitPrice { get; set; }
        public decimal ServiceUnitPrice { get; set; }
        public decimal LinkUnitPrice { get; set; }
        public decimal PurchasePrice { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
