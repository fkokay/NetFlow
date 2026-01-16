using NetFlow.Domain.Netsis.Shipments;
using NetFlow.Domain.Netsis.Warehouses;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Warehouses
{
    public interface IWarehouseReadRepository
    {
        Task<List<Warehouse>> GetWarehouses();
    }
}
