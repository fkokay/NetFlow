using NetFlow.Domain.Netsis.Shipments;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Shipments
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
