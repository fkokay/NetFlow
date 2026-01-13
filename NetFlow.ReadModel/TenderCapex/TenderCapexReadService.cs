using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.ReadModel;
using NetFlow.ReadModel.TenderCapex;

public sealed class TenderCapexReadService
{
    private readonly ReadModelOptions _opt;
    public TenderCapexReadService(ReadModelOptions opt) => _opt = opt;

    public async Task<IReadOnlyList<TenderCapexDto>> ListAsync(int tenderId)
    {
        using var cn = new SqlConnection(_opt.ConnectionString);

        var sql = """
        SELECT *
        FROM dbo.VW_TenderCapex WITH(NOLOCK)
        WHERE TenderId = @TenderId
        ORDER BY UnitName, AssetCode
        """;

        return (await cn.QueryAsync<TenderCapexDto>(sql, new { TenderId = tenderId })).AsList();
    }

    public async Task<TenderCapexDto?> GetAsync(int id)
    {
        using var cn = new SqlConnection(_opt.ConnectionString);
        return await cn.QueryFirstOrDefaultAsync<TenderCapexDto>(
            "SELECT * FROM dbo.VW_TenderCapex WHERE Id=@Id", new { Id = id });
    }
}
