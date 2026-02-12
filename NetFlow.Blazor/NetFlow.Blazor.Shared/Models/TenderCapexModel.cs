using NetFlow.Blazor.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFlow.Blazor.Shared.Models
{
    public class TenderCapexModel
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int TenderAuthorityId { get; set; }
        public string? ParentAuthorityCode { get; set; }
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = "Adet";
        public decimal PurchasePrice { get; set; }
        public string? Description { get; set; }
        public string StockCode { get; set; } = string.Empty;
        public string StockName { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public int? MaterialRequestId { get; set; }
        public string? MaterialRequestNo { get; set; }
        public MaterialRequestType RequestType { get; set; } = MaterialRequestType.Tender;
        public MaterialRequestPriority Priority { get; set; } = MaterialRequestPriority.Normal;
        public MaterialRequestStatus MaterialRequestStatus { get; set; } = Shared.Models.MaterialRequestStatus.Draft;
        public int? MaterialRequestItemId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public decimal TotalAmount => Quantity * (PurchasePrice);
    }
}

