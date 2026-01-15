namespace NetFlow.Blazor.Shared.Models
{
    public class TenderReaktifModel
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int TenderAuthorityId { get; set; }
        public string ParentAuthorityCode { get; set; } = string.Empty;
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public string StockCode { get; set; } = string.Empty;
        public string SutCode { get; set; } = string.Empty;
        public string? TestName { get; set; }
        public decimal TestCount { get; set; } = 0m;
        public decimal SutPoint { get; set; } = 0m;
        public decimal TotalSutPoint { get; set; } = 0m;
        public string Currency { get; set; } = "TRY";
        public decimal UnitPrice { get; set; } = 0m;
        public class TenderRequiredDocument
        {
            public int Id { get; set; }
            public int TenderId { get; set; }
            public int DocumentId { get; set; }
            public bool IsMandatory { get; set; } = true;
            public bool Submitted { get; set; } = false;
            public DateTime? SubmissionDate { get; set; }
            public string DocumentName { get; set; } = string.Empty;
            public string? FileName { get; set; }
            public int? TenderRequiredDocumentFileId { get; set; }
            public string? FileType { get; set; }
            public byte[]? FileContent { get; set; }
        }
    }
}
