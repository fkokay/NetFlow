using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Domain.Common.Pagination;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;
using System.Xml.Linq;


namespace NetFlow.ReadModel.Tenders
{
    public sealed class TenderReadService
    {
        private readonly ReadModelOptions _opt;

        public TenderReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<PagedResult> ListAsync(int firmId, PagedRequest pagedRequest)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("FirmId", firmId);

            string whereSql = "WHERE FirmId = @FirmId";
            if (!string.IsNullOrEmpty(pagedRequest.Filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.Filter);
                whereSql += " AND " + sql;
                parameters.AddDynamicParams(p);
            }
            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.Sort, "ORDER BY Id DESC");
            string countSql = $@"
                SELECT COUNT(1) FROM dbo.VW_Tender WITH (NOLOCK)
                {whereSql}
            ";

            string dataSql = $@"
                SELECT * FROM dbo.VW_Tender WITH (NOLOCK)
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
                    data = Array.Empty<TenderDto>(),
                    totalCount = totalCount
                };
            }

            if (!string.IsNullOrWhiteSpace(pagedRequest.TotalSummary))
            {
                var (summarySqlPart, aliases) =
                    DevExtremeSqlBuilder.BuildSummary(pagedRequest.TotalSummary);

                string sql = $@"
                    SELECT {summarySqlPart}
                    FROM dbo.VW_Tender WITH (NOLOCK)
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

            var data = cn.Query<TenderDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                data = data,
                totalCount = totalCount
            };
        }

        public async Task<TenderDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_Tender WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<TenderDto>(sql, new { Id = id });
        }
    }


}
