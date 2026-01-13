using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Assets
{
    public sealed class AssetReadService
    {
        private readonly ReadModelOptions _opt;
        public AssetReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<IReadOnlyList<AssetDto>> ListAsync(int? tenderId)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = """
        SELECT *
        FROM dbo.VW_Asset WITH(NOLOCK)
        WHERE (@TenderId IS NULL OR TenderId=@TenderId)
        ORDER BY Id DESC
        """;

            return (await cn.QueryAsync<AssetDto>(sql, new { TenderId = tenderId })).AsList();
        }

        public async Task<AssetDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            return await cn.QueryFirstOrDefaultAsync<AssetDto>(
                "SELECT * FROM dbo.VW_Asset WHERE Id=@Id", new { Id = id });
        }
    }
}
