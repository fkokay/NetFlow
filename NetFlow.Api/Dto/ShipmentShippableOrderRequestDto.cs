namespace NetFlow.Api.Dto
{
    public class ShipmentShippableOrderRequestDto
    {
        public string? Cari { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Depo { get; set; }
        public bool HasBalance { get; set; }
    }
}
