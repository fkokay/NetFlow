using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.DevExtreme;
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
            if (!string.IsNullOrEmpty(pagedRequest.Filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.Filter);
                whereSql += " AND " + sql;
                parameters.AddDynamicParams(p);
            }
            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.Sort, "ORDER BY Id DESC");
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

            if (pagedRequest.IsCountQuery != null && pagedRequest.IsCountQuery.HasValue)
            {
                return new PagedResult
                {
                    Data = Array.Empty<GuaranteeCommissionDto>(),
                    TotalCount = totalCount
                };
            }
            parameters.Add("@Skip", pagedRequest.Skip ?? 0);
            parameters.Add("@Take", pagedRequest.Take ?? 10);
            var data = cn.Query<GuaranteeCommissionDto>(dataSql, parameters).ToList();
            return new PagedResult
            {
                Data = data,
                TotalCount = totalCount
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
