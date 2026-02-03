namespace NetFlow.Blazor.Shared.Models
{
    public partial class TenderReaktifModel
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
        public decimal TestCount { get; set; } = 0m;
        public decimal SutPoint { get; set; } = 0m;
        public decimal TotalSutPoint { get; set; } = 0m;
        public string Currency { get; set; } = "TRY";
        public decimal UnitPrice { get; set; } = 0m;
        public int? MaterialRequestId { get; set; }
        public string? MaterialRequestNo { get; set; }
        public MaterialRequestType RequestType { get; set; } = MaterialRequestType.Production;
        public MaterialRequestPriority Priority { get; set; } = MaterialRequestPriority.Normal;
        public MaterialRequestStatus MaterialRequestStatus { get; set; } = Shared.Models.MaterialRequestStatus.Draft;
        public int? MaterialRequestItemId { get; set; }
    }
}
