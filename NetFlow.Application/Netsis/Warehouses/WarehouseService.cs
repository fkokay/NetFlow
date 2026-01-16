using NetFlow.Application.Netsis.Shipments;
using NetFlow.Domain.Netsis.Shipments;
using NetFlow.Domain.Netsis.Warehouses;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Warehouses
{
    public class WarehouseService
    {
        private readonly IWarehouseReadRepository _readRepo;

        public WarehouseService(IWarehouseReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<List<Warehouse>> GetWarehouses()
        {
            return await _readRepo.GetWarehouses();
        }
    }
}
