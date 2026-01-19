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
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public string StockCode { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
