using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.Firms;
using NetFlow.ReadModel.Guarantees;
using NetFlow.ReadModel.Tenders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Roles
{
    public class RoleReadService
    {
        private readonly ReadModelOptions _opt;
        public RoleReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<PagedResult> ListAsync(int firmId, PagedRequest pagedRequest)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(pagedRequest.Filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.Filter);
                parameters.AddDynamicParams(p);
            }
            string orderBy = "ORDER BY "+ DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.Sort, "Id DESC");
            string countSql = $@"
                SELECT COUNT(1) FROM Role (NOLOCK)
            ";

            string dataSql = $@"
                SELECT * FROM Role WITH (NOLOCK)
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
                    Data = Array.Empty<RoleDto>(),
                    TotalCount = totalCount
                };
            }

            parameters.Add("@Skip", pagedRequest.Skip ?? 0);
            parameters.Add("@Take", pagedRequest.Take ?? 10);

            var data = cn.Query<RoleDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                Data = data,
                TotalCount = totalCount
            };
        }

        public async Task<List<RoleSelectDto>> GetRoleListAsync()
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            const string sql = @"
                SELECT
                    Id,
                    Name
                FROM Role WITH (NOLOCK)
                ORDER BY Name
            ";

            var data = await cn.QueryAsync<RoleSelectDto>(sql);
            return data.ToList();
        }
        public async Task<RoleDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var sql = "SELECT TOP 1 * FROM Role WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<RoleDto>(sql, new { Id = id });
        }

    }
}
