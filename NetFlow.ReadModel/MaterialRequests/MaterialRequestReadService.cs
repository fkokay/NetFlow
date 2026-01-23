using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Identity;
using NetFlow.ReadModel.Tenders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Requests
{
    public sealed class MaterialRequestReadService
    {
        private readonly ReadModelOptions _opt;

        public MaterialRequestReadService(ReadModelOptions opt) => _opt = opt;
        public async Task<PagedResult> ListAsync(int userId,int firmId, PagedRequest pagedRequest,bool open=false,bool closed=false,bool my=false)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("FirmId", firmId);

            string whereSql = "WHERE FirmId = @FirmId";
            if (open)
            {
                whereSql += " AND Status = @Status";
                parameters.Add("Status", "Open");
            }
            if (closed)
            {
                whereSql += " AND Status = @Status";
                parameters.Add("Status", "Closed");
            }
            if (my)
            {
                whereSql += " AND ApprovedByUserId = @ApprovedByUserId";
                parameters.Add("ApprovedByUserId", userId);
            }
            if (!string.IsNullOrEmpty(pagedRequest.Filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.Filter);
                whereSql += " AND " + sql;
                parameters.AddDynamicParams(p);
            }
            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.Sort, "ORDER BY Id DESC");
            string countSql = $@"
                SELECT COUNT(1) FROM dbo.VW_MaterialRequest WITH (NOLOCK)
                {whereSql}
            ";

            string dataSql = $@"
                SELECT * FROM dbo.VW_MaterialRequest WITH (NOLOCK)
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
                    data = Array.Empty<MaterialRequestDto>(),
                    totalCount = totalCount
                };
            }

            if (!string.IsNullOrWhiteSpace(pagedRequest.TotalSummary))
            {
                var (summarySqlPart, aliases) =
                    DevExtremeSqlBuilder.BuildSummary(pagedRequest.TotalSummary);

                string sql = $@"
                    SELECT {summarySqlPart}
                    FROM dbo.VW_MaterialRequest WITH (NOLOCK)
                    {whereSql};
                ";

                var row = await cn.QuerySingleAsync(sql, parameters);

                var values = aliases
                    .Select(a => ((IDictionary<string, object>)row)[a])
                    .ToArray();

                return new PagedResult
                {
                    data = Array.Empty<object>(),
                    totalCount = totalCount,
                    summary = values
                };
            }

            parameters.Add("@Skip", pagedRequest.Skip ?? 0);
            parameters.Add("@Take", pagedRequest.Take ?? 10);

            var data = cn.Query<MaterialRequestDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                data = data,
                totalCount = totalCount
            };
        }

        public async Task<MaterialRequestDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_MaterialRequest WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<MaterialRequestDto>(sql, new { Id = id });
        }
    }
}
