using NetFlow.Domain.Shipping;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Shipping
{
    public interface IShipmentReadRepository
    {
        Task<List<ShipmentOrder>> GetShippableOrders(ShipmentShippableOrderFilter filter);
    }
}
