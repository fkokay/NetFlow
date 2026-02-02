using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("TenderReaktif")]
    public class TenderReaktifEntity
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int? TenderAuthorityId { get; set; }
        public string StockCode { get; set; } = null!;
        public string SutCode { get; set; } = null!;
        public string? TestName { get; set; }
        public decimal TestCount { get; set; }
        public decimal SutPoint { get; set; }
        public decimal TotalSutPoint { get; set; }
        public string Currency { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int? MaterialRequestId { get; set; }
        public int? MaterialRequestItemId { get; set; }

    }
}
