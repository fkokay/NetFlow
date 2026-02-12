using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderDevices
{
    public class EditTenderDeviceRequest
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int TenderAuthorityId { get; set; }
        public string SupplyType { get; set; }
        public string StockCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal RentPrice { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal LinkPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Currency { get; set; }
        public string? Description { get; set; }
        public int? MaterialRequestId { get; set; }
        public int? MaterialRequestItemId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
