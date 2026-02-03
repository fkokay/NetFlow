using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderDevices
{
    public sealed class TenderDeviceDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public string? SupplyType { get; set; }
        public string? StockCode { get; set; }
        public string? StockName { get; set; }
        public int Quantity { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Currency { get; set; }
        public decimal RentUnitPrice { get; set; }
        public decimal ServiceUnitPrice { get; set; }
        public decimal LinkUnitPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public int? MaterialRequestId { get; set; }
        public string? MaterialRequestNo { get; set; }
        public MaterialRequestStatus MaterialRequestStatus { get; set; } = MaterialRequestStatus.Open;
        public int? MaterialRequestItemId { get; set; }
        public DateTime CreatedAt { get; set; }
       
    }
}
