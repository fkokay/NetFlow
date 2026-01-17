using Dapper;
using NetFlow.Application.Netsis.Orders;
using NetFlow.Domain.Netsis.Orders;
using NetFlow.Domain.Netsis.Warehouses;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisOrderReadRepository : IOrderReadRepository
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory;

        public NetsisOrderReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }

        public async Task<List<Order>> GetOrders()
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Warehouses.sql");

            var dto = await con.QueryAsync<OrderDto>(sql);

            return dto.Select(x =>
                      Order.Create(
                          x.SUBE_KODU,
                          x.FTIRSIP,
                          x.FATIRS_NO,
                          x.CARI_KODU,
                          x.CARI_ISIM,
                          x.TARIH,
                          x.TIPI,
                          x.ACIKLAMA,
                          x.GENELTOPLAM
                      )).ToList();
        }
    }
}
