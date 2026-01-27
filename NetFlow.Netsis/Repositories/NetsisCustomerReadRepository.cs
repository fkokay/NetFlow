using Dapper;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Application.Netsis.Customers;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Customers;
using NetFlow.Domain.Netsis.ExpenseAccountCodes;
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
    public class NetsisCustomerReadRepository : ICustomerReadRepository
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory; 
        Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["BranchCode"] = "SUBE_KODU",
            ["BusinessCode"] = "ISLETME_KODU",
            ["Code"] = "CARI_KOD",
            ["Name"] = "CARI_ISIM",
        };

        public NetsisCustomerReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }
        public async Task<List<Customer>> GetCustomers()
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Customers.sql");

            var dto = await con.QueryAsync<CustomerDto>(sql);

            return dto.Select(NetsisUtils.FixAllStrings).Select(x =>
                      Customer.Create(
                          x.SUBE_KODU,
                          x.ISLETME_KODU,
                          x.CARI_KOD,
                          x.CARI_ISIM
                      )).ToList();
        }

        public async Task<PagedResult> GetCustomers(PagedRequest request)
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Customers.sql");
            var sqlCount = _sql.Get("CustomersCount.sql");
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

            var dto = con.Query<CustomerDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                TotalCount = totalCount,
                Data = dto.Select(NetsisUtils.FixAllStrings).Select(x =>
                      Customer.Create(
                          x.SUBE_KODU,
                          x.ISLETME_KODU,
                          x.CARI_KOD,
                          x.CARI_ISIM
                      )).ToList()
            };
        }
    }
}
