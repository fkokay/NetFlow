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

        public string ParentAuthorityCode { get; set; }

        public string UnitCode { get; set; }
        public string UnitName { get; set; }

        public string StockCode { get; set; }
        public string SutCode { get; set; }
        public string TestName { get; set; }

        public decimal TestCount { get; set; }
        public decimal SutPoint { get; set; }
        public decimal TotalSutPoint { get; set; }

        public string Currency { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
