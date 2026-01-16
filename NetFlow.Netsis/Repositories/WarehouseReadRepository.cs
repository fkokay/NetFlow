using Dapper;
using NetFlow.Application.Netsis.Warehouses;
using NetFlow.Domain.Netsis.Shipments;
using NetFlow.Domain.Netsis.Warehouses;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class WarehouseReadRepository : IWarehouseReadRepository   
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory;

        public WarehouseReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }

        public async Task<List<Warehouse>> GetWarehouses()
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Warehouses.sql");

            var dto = await con.QueryAsync<WarehouseDto>(sql);

            return dto.Select(x =>
                      Warehouse.Create(
                          x.SUBE_KODU,
                          x.DEPO_KODU,
                          x.DEPO_ISMI
                      )).ToList();
        }
    }
}
