using Dapper;
using NetFlow.Application.Shipping;
using NetFlow.Domain.Shipping;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisShipmentReadRepository : IShipmentReadRepository
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory;

        public NetsisShipmentReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }

        public async Task<List<ShipmentOrder>> GetShippableOrders(ShipmentShippableOrderFilter f)
        {
            using var con = _factory.Create();

            var sql = _sql.Get("ShippableOrders.sql");

            var dto = await con.QueryAsync<ShipmentShippableOrderDto>(sql, new
            {
                CARI_KODU = f.CustomerCode,
                BASTAR = f.StartDate,
                BITTAR = f.EndDate,
                DEPO_KODU = f.Warehouse,
                HAS_BALANCE = f.HasBalance ? 1 : 0
            });

            return dto.Select(x =>
                      ShipmentOrder.Create(
                          x.SIPARIS_NO,
                          x.STOK_KODU,
                          x.MIKTAR,
                          x.DEPO_KODU,
                          x.DEPO_BAKIYE
                      )).ToList();
        }
    }
}
