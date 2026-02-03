using Dapper;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Application.Netsis.Products;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Customers;
using NetFlow.Domain.Netsis.Products;
using NetFlow.Domain.Netsis.Warehouses;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using NetFlow.Netsis.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisProductReadRepository : IProductReadRepository
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory;
        Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["BranchCode"] = "SUBE_KODU",
            ["BusinessCode"] = "ISLETME_KODU",
            ["Code"] = "STOK_KODU",
            ["Name"] = "STOK_ADI",
        };
        public NetsisProductReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }

        public async Task<List<Product>> GetProducts()
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Products.sql");

            var dto = await con.QueryAsync<ProductDto>(sql);

            return dto.Select(x =>
                      Product.Create(
                          x.SUBE_KODU,
                          x.ISLETME_KODU,
                          x.STOK_KODU,
                          x.STOK_ADI
                      )).ToList();
        }

        public async Task<PagedResult> GetProducts(PagedRequest request)
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Products.sql");
            var sqlCount = _sql.Get("ProductsCount.sql");
            string whereSql = "WHERE 1=1";
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var (filteSql, p) = DevExtremeSqlBuilder.Compile(request.Filter, fieldMap);
                whereSql += " AND " + filteSql;
                parameters.AddDynamicParams(p);
            }

            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(request.Sort, "ORDER BY SUBE_KODU DESC", fieldMap);
            parameters.Add("@Skip", request.Skip ?? 0);
            parameters.Add("@Take", request.Take ?? 10);

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

            if (request.IsCountQuery != null && request.IsCountQuery.HasValue)
            {
                return new PagedResult
                {
                    TotalCount = totalCount,
                    Data = Array.Empty<Customer>(),
                };
            }

            var dto = con.Query<ProductDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                TotalCount = totalCount,
                Data = dto.Select(NetsisUtils.FixAllStrings).Select(x =>
                      Product.Create(
                          x.SUBE_KODU,
                          x.ISLETME_KODU,
                          x.STOK_KODU,
                          x.STOK_ADI
                      )).ToList()
            };
        }
    }
}
