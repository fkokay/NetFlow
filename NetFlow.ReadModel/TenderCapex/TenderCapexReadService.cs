using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel;
using NetFlow.ReadModel.Assets;
using NetFlow.ReadModel.TenderAuthorities;
using NetFlow.ReadModel.TenderCapex;

public sealed class TenderCapexReadService
{
    private readonly ReadModelOptions _opt;
    public TenderCapexReadService(ReadModelOptions opt) => _opt = opt;



    public async Task<PagedResult> ListAsync(int tenderId, PagedRequest pagedRequest)
    {
        using var cn = new SqlConnection(_opt.ConnectionString);
        var parameters = new DynamicParameters();

        parameters.Add("TenderId", tenderId);

        string whereSql = "WHERE TenderId = @TenderId";

        if (!string.IsNullOrEmpty(pagedRequest.Filter))
        {
            var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.Filter);
            whereSql += " AND " + sql;
            parameters.AddDynamicParams(p);
        }

        string orderBy = "ORDER BY "+DevExtremeSqlBuilder.BuildOrderBy(
            pagedRequest.Sort,
            "Id DESC"
        );

        string countSql = $@"
            SELECT COUNT(1)
            FROM dbo.VW_TenderCapex WITH (NOLOCK)
            {whereSql}
        ";

        int totalCount = cn.ExecuteScalar<int>(
            countSql,
            parameters
        );

        if (pagedRequest.IsCountQuery == true)
        {
            return new PagedResult
            {
                data = Array.Empty<TenderCapexDto>(),
                totalCount = totalCount
            };
        }

        parameters.Add("@Skip", pagedRequest.Skip ?? 0);
        parameters.Add("@Take", pagedRequest.Take ?? 10);

        string dataSql = $@"
        SELECT *
        FROM dbo.VW_TenderCapex WITH (NOLOCK)
        {whereSql}
        {orderBy}
        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
    ";

        var data = cn.Query<TenderCapexDto>(
            dataSql,
            parameters
        ).ToList();

        return new PagedResult
        {
            data = data,
            totalCount = totalCount
        };
    }

    public async Task<TenderAuthorityDto?> GetAsync(int id)
    {
        using var cn = new SqlConnection(_opt.ConnectionString);
        var sql = "SELECT TOP 1 * FROM dbo.VW_TenderCapex WITH (NOLOCK) WHERE Id=@Id";
        return await cn.QueryFirstOrDefaultAsync<TenderAuthorityDto>(sql, new { Id = id });
    }
}
