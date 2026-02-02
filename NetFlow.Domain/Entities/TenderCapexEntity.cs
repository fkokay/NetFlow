using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("TenderCapex")]
    public class TenderCapexEntity
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int? TenderAuthorityId { get; set; }
        public string AssetCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public int? MaterialRequestId { get; set; }
        public int? MaterialRequestItemId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
