using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.Utils;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.Guarantees;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.GuaranteeCommissions
{
    public sealed class GuaranteeCommissionReadService
    {
        private readonly ReadModelOptions _opt;
        public GuaranteeCommissionReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<PagedResult> ListAsync(int guaranteeId, PagedRequest pagedRequest)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("GuaranteeId", guaranteeId);

            string whereSql = "WHERE GuaranteeId = @GuaranteeId";
            if (!string.IsNullOrEmpty(pagedRequest.filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.filter);
                whereSql += " AND " + sql;
                parameters.AddDynamicParams(p);
            }
            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.sort, "ORDER BY Id DESC");
            string countSql = $@"
                SELECT COUNT(1) FROM dbo.VW_GuaranteeCommission WITH (NOLOCK)
                {whereSql}
            ";

            string dataSql = $@"
                SELECT * FROM dbo.VW_GuaranteeCommission WITH (NOLOCK)
                {whereSql}
                {orderBy}
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";

            int totalCount = cn.ExecuteScalar<int>(
                countSql, parameters
            );

            if (pagedRequest.isCountQuery != null && pagedRequest.isCountQuery.HasValue)
            {
                return new PagedResult
                {
                    data = Array.Empty<GuaranteeCommissionDto>(),
                    totalCount = totalCount
                };
            }
            parameters.Add("@Skip", pagedRequest.skip ?? 0);
            parameters.Add("@Take", pagedRequest.take ?? 10);
            var data = cn.Query<GuaranteeCommissionDto>(dataSql, parameters).ToList();
            return new PagedResult
            {
                data = data,
                totalCount = totalCount
            };
        }
        public async Task<GuaranteeCommissionDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_GuaranteeCommission WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<GuaranteeCommissionDto>(sql, new { Id = id });
        }
    }
}
