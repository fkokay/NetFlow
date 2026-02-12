using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderOpex
{
    public sealed class TenderOpexDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int? TenderAuthorityId { get; set; }
        public string? ParentAuthorityCode { get; set; }
        public string StockCode { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; }
        public decimal TotalAmount { get; private set; }
        public string? Description { get; set; }
        public int? MaterialRequestId { get; set; }
        public string? MaterialRequestNo { get; set; }
        public MaterialRequestStatus MaterialRequestStatus { get; set; } = MaterialRequestStatus.Open;
        public int? MaterialRequestItemId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
