using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.Pagination;
using NetFlow.Application.Common.Utils;
using NetFlow.ReadModel.Guarantees;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Firms
{
    public sealed class FirmReadService
    {
        private readonly ReadModelOptions _opt;
        public FirmReadService(ReadModelOptions opt) => _opt = opt;

       public async Task<PagedResult> ListAsync(PagedRequest pagedRequest)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();

            string whereSql = string.Empty;

            if (!string.IsNullOrEmpty(pagedRequest.filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.filter);
                whereSql = "WHERE " + sql;
                parameters.AddDynamicParams(p);
            }

            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(
                pagedRequest.sort,
                "Id DESC"
            );

            string countSql = $@"
                SELECT COUNT(1)
                FROM dbo.VW_Firm WITH (NOLOCK)
                {whereSql}
            ";

            int totalCount = cn.ExecuteScalar<int>(
                countSql,
                parameters
            );

            if (pagedRequest.isCountQuery != null && pagedRequest.isCountQuery.HasValue)
            {
                return new PagedResult
                {
                    data = Array.Empty<FirmDto>(),
                    totalCount = totalCount
                };
            }

            parameters.Add("@Skip", pagedRequest.skip ?? 0);
            parameters.Add("@Take", pagedRequest.take ?? 10);

            string dataSql = $@"
                SELECT *
                FROM dbo.VW_Firm WITH (NOLOCK)
                {whereSql}
                ORDER BY {orderBy}
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";

            var data = cn.Query<FirmDto>(
                dataSql,
                parameters
            ).ToList();

            return new PagedResult
            {
                data = data,
                totalCount = totalCount
            };
        }
        public async Task<FirmDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_Firm WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<FirmDto>(sql, new { Id = id });
        }
    }
}
