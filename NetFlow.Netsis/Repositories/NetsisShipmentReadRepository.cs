using Dapper;
using NetFlow.Application.Netsis.Shipments;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Shipments;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using NetFlow.Netsis.Utils;
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

        public async Task<PagedResult> GetShippableOrders(ShipmentShippableOrderFilter filter)
        {
            using var con = _factory.Create();

            var sql = _sql.Get("ShippableOrders.sql");

            var dto = await con.QueryAsync<ShipmentShippableOrderDto>(sql, new
            {
                CARI_KODU = filter.Customer,
                BASTAR = filter.StartDate,
                BITTAR = filter.EndDate,
                DEPO_KODU = filter.Warehouse,
                HAS_BALANCE = filter.HasBalance ? 1 : 0
            });

            if (filter.IsCountQuery != null && filter.IsCountQuery.HasValue)
            {
                return new PagedResult
                {
                    Data = Array.Empty<ShipmentOrder>(),
                    TotalCount = dto.Count(),
                };
            }

            return new PagedResult()
            {
                Data = dto.Select(NetsisUtils.FixAllStrings).Select(x =>
                      ShipmentOrder.Create(
                          x.ID,
                          x.SIPARIS_NO,
                          x.CARI_KODU,
                          x.CARI_ADI,
                          x.STOK_KODU,
                          x.STOK_ADI,
                          x.MIKTAR,
                          x.DEPO_KODU,
                          x.DEPO_BAKIYE
                      )).ToList(),
                TotalCount = dto.Count(),
            };
        }
    }
}
