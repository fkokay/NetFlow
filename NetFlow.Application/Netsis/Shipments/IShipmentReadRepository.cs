using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Shipments;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Shipments
{
    public interface IShipmentReadRepository
    {
        Task<PagedResult> GetShippableOrders(ShipmentShippableOrderFilter filter);
    }
}
