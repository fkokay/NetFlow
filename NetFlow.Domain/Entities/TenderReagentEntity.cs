using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("TenderReagent")]
    public class TenderReagentEntity
    {
        [Key]
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int TenderAuthorityId { get; set; }
        public string StockCode { get; set; }
        public string SutCode { get; set; }
        public string? TestName { get; set; }
        public decimal Quantity { get; set; }
        public decimal SutPoint { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalSutPoint { get; private set; }
        public string Currency { get; set; }
        public decimal UnitPrice { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalAmount { get; private set; }
        public decimal PurchasePrice { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalPurchaseAmount { get; private set; }
        public string? Description { get; set; }
        public int? MaterialRequestId { get; set; }
        public int? MaterialRequestItemId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
