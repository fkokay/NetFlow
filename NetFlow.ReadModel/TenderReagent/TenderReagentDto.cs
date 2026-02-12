using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderReaktif
{
    public sealed class TenderReagentDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int TenderAuthorityId { get; set; }
        public string ParentAuthorityCode { get; set; } = string.Empty;
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public string StockCode { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
        public string SutCode { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal TestCount { get; set; }
        public decimal SutPoint { get; set; }
        public decimal TotalSutPoint { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; private set; }
        public decimal PurchasePrice { get; set; }
        public decimal TotalPurchaseAmount { get; private set; }
        public string? Description { get; set; }
        public int? MaterialRequestId { get; set; }
        public string? MaterialRequestNo { get; set; }
        public MaterialRequestStatus MaterialRequestStatus { get; set; } = MaterialRequestStatus.Open;
        public int? MaterialRequestItemId { get; set; }
    }
}
