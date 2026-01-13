using NetFlow.Domain.Shipping;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Shipping
{
    public sealed class ShipmentService
    {
        private readonly IShipmentReadRepository _readRepo;

        public ShipmentService(IShipmentReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<List<ShipmentOrder>> GetShippableOrders(ShipmentShippableOrderFilter filter)
        {
            return await _readRepo.GetShippableOrders(filter);
        }
    }
}
