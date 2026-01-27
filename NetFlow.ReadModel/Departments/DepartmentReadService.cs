using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.Firms;

namespace NetFlow.ReadModel.Departments
{
    public class DepartmentReadService
    {
        private readonly ReadModelOptions _opt;
        public DepartmentReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<PagedResult> ListAsync(PagedRequest pagedRequest)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(pagedRequest.Filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.Filter);
                parameters.AddDynamicParams(p);
            }

            var orderBy = DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.Sort, "ORDER BY Id DESC");

            string countSql = @"
                 SELECT COUNT(1) FROM Department WITH (NOLOCK)
             ";

            string dataSql = $@"
                 SELECT * FROM Department WITH (NOLOCK)
                 {orderBy}
                 OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
             ";

            int totalCount = cn.ExecuteScalar<int>(countSql, parameters);

            if (pagedRequest.IsCountQuery != null && pagedRequest.IsCountQuery.HasValue)
            {
                return new PagedResult
                {
                    Data = Array.Empty<DepartmentDto>(),
                    TotalCount = totalCount
                };
            }

            parameters.Add("@Skip", pagedRequest.Skip ?? 0);
            parameters.Add("@Take", pagedRequest.Take ?? 10);

            var data = cn.Query<DepartmentDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                Data = data,
                TotalCount = totalCount
            };
        }

        public async Task<List<DepartmentDto>> GetDepartmentListAsync()
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            const string sql = @"
                SELECT * FROM Department WITH (NOLOCK) ORDER BY DepartmentCode   
            ";

            var data = await cn.QueryAsync<DepartmentDto>(sql);
            return data.ToList();
        }

        public async Task<DepartmentDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM Department WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<DepartmentDto>(sql, new { Id = id });
        }
    }
}
