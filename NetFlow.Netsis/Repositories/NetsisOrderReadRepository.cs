using Dapper;
using NetFlow.Application.Common.Utils;
using NetFlow.Application.Netsis.Orders;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Orders;
using NetFlow.Domain.Netsis.Warehouses;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using NetFlow.Netsis.Utils;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisOrderReadRepository : IOrderReadRepository
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory;
        Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["BranchCode"] = "SUBE_KODU",
            ["OrderType"] = "FTIRSIP",
            ["OrderNumber"] = "FATIRS_NO",
            ["CustomerCode"] = "CARI_KODU",
            ["CustomerName"] = "CARI_ISIM",
            ["OrderDate"] = "TARIH",
            ["OrderStatus"] = "TIPI",
            ["Description"] = "ACIKLAMA",
            ["NetTotal"] = "GENELTOPLAM",
        };

        public NetsisOrderReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }

        public async Task<PagedResult> GetOrders(short orderType, PagedRequest request)
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Orders.sql");
            var sqlCount = _sql.Get("OrdersCount.sql");

            string whereSql = "WHERE SIPAMAS.FTIRSIP = @FTIRSIP";
            var parameters = new DynamicParameters();
            parameters.Add("@FTIRSIP", orderType);

            if (!string.IsNullOrEmpty(request.filter))
            {
                var (filteSql, p) = DevExtremeSqlBuilder.Compile(request.filter, fieldMap);
                whereSql += " AND " + filteSql;
                parameters.AddDynamicParams(p);
            }

            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(request.sort, "ORDER BY SIPAMAS.TARIH DESC", fieldMap);
            parameters.Add("@Skip", request.skip ?? 0);
            parameters.Add("@Take", request.take ?? 10);

            string dataSql = $@"
                {sql}
                {whereSql}
                {orderBy}
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";

            string countSql = $@"
                {sqlCount}
                {whereSql}
            ";

            int totalCount = con.ExecuteScalar<int>(
                countSql, parameters
            );

            if (request.isCountQuery != null && request.isCountQuery.HasValue)
            {
                return new PagedResult
                {
                    totalCount = totalCount,
                    data = Array.Empty<Order>(),
                };
            }

            var dto = con.Query<OrderDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                totalCount = totalCount,
                data = dto.Select(NetsisUtils.FixAllStrings).Select(x =>
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
                      )).ToList()
            };
        }
    }
}
