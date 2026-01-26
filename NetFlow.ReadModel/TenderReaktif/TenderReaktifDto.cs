using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderReaktif
{
    public sealed class TenderReaktifDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int TenderAuthorityId { get; set; }

        public string ParentAuthorityCode { get; set; } = string.Empty;

        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public string StockCode { get; set; } = string.Empty;
        public string SutCode { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;

        public decimal TestCount { get; set; }
        public decimal SutPoint { get; set; }
        public decimal TotalSutPoint { get; set; }

        public string Currency { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
