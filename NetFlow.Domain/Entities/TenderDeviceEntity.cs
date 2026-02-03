using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{

    [Table("TenderDevice")]
    public class TenderDeviceEntity
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int? TenderAuthorityId { get; set; }
        public string? SupplyType { get; set; }
        public string? StockCode { get; set; }
        public int Quantity { get; set; }
        public string? CustomerCode { get; set; }
        public string? Currency { get; set; }
        public decimal RentUnitPrice { get; set; }
        public decimal ServiceUnitPrice { get; set; }
        public decimal LinkUnitPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? MaterialRequestId { get; set; }
        public int? MaterialRequestItemId { get; set; }
    }
}
