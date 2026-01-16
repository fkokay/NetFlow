using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Shipments
{
    public class ShipmentShippableOrderFilter
    {
        public string? CustomerCode { get; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public string? Warehouse { get; }
        public bool HasBalance { get; }

        public ShipmentShippableOrderFilter(
            string? customerCode,
            DateTime? startDate,
            DateTime? endDate,
            string? warehouse,
            bool hasBalance)
        {
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
                throw new InvalidShipmentDateException();

            CustomerCode = customerCode;
            StartDate = startDate;
            EndDate = endDate;
            Warehouse = warehouse;
            HasBalance = hasBalance;
        }
    }
}
