using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.Tenders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Guarantees
{
    public sealed class GuaranteeReadService
    {
        private readonly ReadModelOptions _opt;
        public GuaranteeReadService(ReadModelOptions opt) => _opt = opt;


        public async Task<PagedResult> ListAsync(int firmId, PagedRequest pagedRequest,bool expiring = false,bool isActive = false,bool isRefunded = false)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("FirmId", firmId);

            string whereSql = "WHERE FirmId = @FirmId";
            if (expiring)
            {
                whereSql += " AND ExpiryDate BETWEEN GETDATE() AND DATEADD(MONTH,1,GETDATE())";
            }

            if (isActive)
            {
                whereSql += " AND GETDATE() <= ExpiryDate";
            }

            if (isRefunded)
            {
                whereSql += " AND IsRefunded = 1";
            }

            if (!string.IsNullOrEmpty(pagedRequest.Filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.Filter);
                whereSql += " AND " + sql;
                parameters.AddDynamicParams(p);
            }
            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.Sort, "ORDER BY Id DESC");
            string countSql = $@"
                SELECT COUNT(1) FROM dbo.VW_Guarantee WITH (NOLOCK)
                {whereSql}
            ";

            string dataSql = $@"
                SELECT * FROM dbo.VW_Guarantee WITH (NOLOCK)
                {whereSql}
                {orderBy}
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";

            int totalCount = cn.ExecuteScalar<int>(
                countSql, parameters
            );

            if (pagedRequest.IsCountQuery.HasValue && pagedRequest.IsCountQuery.Value)
            {
                return new PagedResult
                {
                    Data = Array.Empty<GuaranteeDto>(),
                    TotalCount = totalCount
                };
            }

            if (!string.IsNullOrWhiteSpace(pagedRequest.TotalSummary))
            {
                var (summarySqlPart, aliases) =
                    DevExtremeSqlBuilder.BuildSummary(pagedRequest.TotalSummary);

                string sql = $@"
                    SELECT {summarySqlPart}
                    FROM dbo.VW_Guarantee WITH (NOLOCK)
                    {whereSql};
                ";

                var row = await cn.QuerySingleAsync(sql, parameters);

                var values = aliases
                    .Select(a => ((IDictionary<string, object>)row)[a])
                    .ToArray();

                return new PagedResult
                {
                    Data = Array.Empty<object>(),
                    TotalCount = totalCount,
                    Summary = values
                };
            }

            parameters.Add("@Skip", pagedRequest.Skip ?? 0);
            parameters.Add("@Take", pagedRequest.Take ?? 10);
            var data = cn.Query<GuaranteeDto>(dataSql, parameters).ToList();
            return new PagedResult
            {
                Data = data,
                TotalCount = totalCount
            };
        }
        public async Task<GuaranteeDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_Guarantee WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<GuaranteeDto>(sql, new { Id = id });
        }
    }
}
