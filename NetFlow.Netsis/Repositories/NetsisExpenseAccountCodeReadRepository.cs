using Dapper;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Application.Netsis.ExpenseAccountCodes;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.ExpenseAccountCodes;
using NetFlow.Domain.Netsis.Orders;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using NetFlow.Netsis.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisExpenseAccountCodeReadRepository:IExpenseAccountCodeReadRepository
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory;
        Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["BranchCode"] = "SUBE_KODU",
            ["Code"] = "HESAP_KODU",
            ["Name"] = "HS_ADI",
        };

        public NetsisExpenseAccountCodeReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }

        public async Task<PagedResult> GetExpenseAccountCodes(PagedRequest request)
        {
            using var con = _factory.Create();

            var sql = _sql.Get("ExpenseAccount.sql");
            var sqlCount = _sql.Get("ExpenseAccountCount.sql");
            string whereSql = "WHERE HESAP_KODU LIKE '%780%' AND AGM='M'";
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var (filteSql, p) = DevExtremeSqlBuilder.Compile(request.Filter, fieldMap);
                whereSql += " AND " + filteSql;
                parameters.AddDynamicParams(p);
            }

            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(request.Sort, "ORDER BY HESAP_KODU DESC", fieldMap);
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
                    Data = Array.Empty<ExpenseAccountCode>(),
                };
            }

            var dto = con.Query<ExpenseAccountCodeDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                TotalCount = totalCount,
                Data = dto.Select(NetsisUtils.FixAllStrings).Select(x =>
                      ExpenseAccountCode.Create(
                          x.SUBE_KODU,
                          x.HESAP_KODU,
                          x.HS_ADI
                      )).ToList()
            };
        }
    }
}
