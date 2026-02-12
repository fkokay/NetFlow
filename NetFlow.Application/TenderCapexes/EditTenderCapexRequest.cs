using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Application.TenderCapexes
{
    public class EditTenderCapexRequest
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int TenderAuthorityId { get; set; }
        public string StockCode { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? MaterialRequestId { get; set; }
        public int? MaterialRequestItemId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
