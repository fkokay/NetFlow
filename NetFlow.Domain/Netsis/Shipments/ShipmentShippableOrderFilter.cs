using NetFlow.Domain.Common.Pagination;

namespace NetFlow.Domain.Netsis.Shipments
{
    public class ShipmentShippableOrderFilter : PagedRequest
    {
        public string? Customer { get; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public short? Warehouse { get; }
        public bool HasBalance { get; }

        public ShipmentShippableOrderFilter(
            string? customer,
            DateTime? startDate,
            DateTime? endDate,
            short? warehouse,
            bool hasBalance)
        {
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
                throw new InvalidShipmentDateException();

            Customer = customer;
            StartDate = startDate;
            EndDate = endDate;
            Warehouse = warehouse;
            HasBalance = hasBalance;
        }
    }
}
