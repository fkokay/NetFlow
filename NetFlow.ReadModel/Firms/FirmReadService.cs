using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Domain.Common.Pagination;
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

            if (!string.IsNullOrEmpty(pagedRequest.Filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.Filter);
                whereSql = "WHERE " + sql;
                parameters.AddDynamicParams(p);
            }

            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(
                pagedRequest.Sort,
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

            if (pagedRequest.IsCountQuery != null && pagedRequest.IsCountQuery.HasValue)
            {
                return new PagedResult
                {
                    data = Array.Empty<FirmDto>(),
                    totalCount = totalCount
                };
            }

            parameters.Add("@Skip", pagedRequest.Skip ?? 0);
            parameters.Add("@Take", pagedRequest.Take ?? 10);

            string dataSql = $@"
                SELECT *
                FROM dbo.VW_Firm WITH (NOLOCK)
                {whereSql}
                {orderBy}
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
        public async Task<List<FirmDto>> GetFirmListAsync()
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            const string sql = @"
                SELECT * FROM dbo.VW_Firm WITH (NOLOCK) ORDER BY FirmCode   
            ";

            var data = await cn.QueryAsync<FirmDto>(sql);
            return data.ToList();
        }

        public async Task<FirmDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_Firm WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<FirmDto>(sql, new { Id = id });
        }
    }
}
