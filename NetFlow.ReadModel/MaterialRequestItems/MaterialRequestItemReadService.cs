using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.MaterialRequestItems
{
    public sealed class MaterialRequestItemReadService
    {
        private readonly ReadModelOptions _opt;

        public MaterialRequestItemReadService(ReadModelOptions opt) => _opt = opt;
        public async Task<PagedResult> ListAsync(int materialRequestId, PagedRequest pagedRequest)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("MaterialRequestId", materialRequestId);

            string whereSql = "WHERE MaterialRequestId = @MaterialRequestId";
            if (!string.IsNullOrEmpty(pagedRequest.Filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.Filter);
                whereSql += " AND " + sql;
                parameters.AddDynamicParams(p);
            }
            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.Sort, "ORDER BY Id DESC");
            string countSql = $@"
                SELECT COUNT(1) FROM dbo.VW_MaterialRequestItem WITH (NOLOCK)
                {whereSql}
            ";

            string dataSql = $@"
                SELECT * FROM dbo.VW_MaterialRequestItem WITH (NOLOCK)
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
                    Data = Array.Empty<MaterialRequestDto>(),
                    TotalCount = totalCount
                };
            }

            if (!string.IsNullOrWhiteSpace(pagedRequest.TotalSummary))
            {
                var (summarySqlPart, aliases) =
                    DevExtremeSqlBuilder.BuildSummary(pagedRequest.TotalSummary);

                string sql = $@"
                    SELECT {summarySqlPart}
                    FROM dbo.VW_MaterialRequestItem WITH (NOLOCK)
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

            var data = cn.Query<MaterialRequestItemDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                Data = data,
                TotalCount = totalCount
            };
        }

        public async Task<MaterialRequestItemDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_MaterialRequestItem WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<MaterialRequestItemDto>(sql, new { Id = id });
        }
    }
}
