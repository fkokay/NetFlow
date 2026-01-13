using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.ReadModel;
using NetFlow.ReadModel.TenderAuthorities;

public sealed class TenderAuthorityReadService
{
    private readonly ReadModelOptions _opt;
    public TenderAuthorityReadService(ReadModelOptions opt) => _opt = opt;

    public async Task<IReadOnlyList<TenderAuthorityDto>> ListAsync(int tenderId)
    {
        using var cn = new SqlConnection(_opt.ConnectionString);

        var sql = """
        SELECT *
        FROM dbo.VW_TenderAuthority WITH(NOLOCK)
        WHERE TenderId = @TenderId
        ORDER BY ParentAuthorityName, UnitName
        """;

        return (await cn.QueryAsync<TenderAuthorityDto>(sql, new { TenderId = tenderId })).AsList();
    }

    public async Task<TenderAuthorityDto?> GetAsync(int id)
    {
        using var cn = new SqlConnection(_opt.ConnectionString);
        return await cn.QueryFirstOrDefaultAsync<TenderAuthorityDto>(
            "SELECT * FROM dbo.VW_TenderAuthority WHERE Id=@Id", new { Id = id });
    }
}
