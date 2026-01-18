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
        public string AssetCode { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public string Unit { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
