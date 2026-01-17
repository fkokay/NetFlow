using NetFlow.Domain.Common.Pagination;

namespace NetFlow.Api.Dto
{
    public class ShipmentShippableOrderRequestDto : PagedRequest
    {
        public string? customer { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public short? warehouse { get; set; }
        public bool hasBalance { get; set; } = false;
    }
}
