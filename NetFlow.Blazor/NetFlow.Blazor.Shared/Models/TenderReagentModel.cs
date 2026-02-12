namespace NetFlow.Blazor.Shared.Models
{
    public partial class TenderReagentModel
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
        public string? TestName { get; set; }
        public decimal Quantity { get; set; }
        public decimal TestCount => Quantity;
        public decimal SutPoint { get; set; }
        public decimal TotalSutPoint => Quantity * (SutPoint);
        public string Currency { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount => Quantity * (UnitPrice);
        public decimal PurchasePrice { get; set; }
        public decimal TotalPurchaseAmount => Quantity * (PurchasePrice);
        public string? Description { get; set; }
        public int? MaterialRequestId { get; set; }
        public string? MaterialRequestNo { get; set; }
        public int? MaterialRequestItemId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public MaterialRequestType RequestType { get; set; } = MaterialRequestType.Tender;
        public MaterialRequestPriority Priority { get; set; } = MaterialRequestPriority.Normal;
        public MaterialRequestStatus MaterialRequestStatus { get; set; } = Shared.Models.MaterialRequestStatus.Draft;
    }

}
