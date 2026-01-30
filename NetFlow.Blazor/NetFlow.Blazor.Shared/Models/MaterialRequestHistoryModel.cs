namespace NetFlow.Blazor.Shared.Models
{
    public class MaterialRequestHistoryModel
    {
        public int Id { get; set; }
        public int MaterialRequestId { get; set; }
        public MaterialRequestHistoryAction Action { get; set; }
        public string? Notes { get; set; }
        public int ActionByUserId { get; set; }
        public string ActionByUserFullName { get; set; }
        public string RequestNo { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;
    }
}
