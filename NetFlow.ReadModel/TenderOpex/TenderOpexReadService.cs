using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderOpex
{
    public sealed class TenderOpexReadService
    {
        private readonly ReadModelOptions _opt;
        public TenderOpexReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<IReadOnlyList<TenderOpexDto>> ListAsync(int tenderId)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = """
        SELECT *
        FROM dbo.VW_TenderOpex WITH(NOLOCK)
        WHERE TenderId = @TenderId
        ORDER BY UnitName, StockCode
        """;

            return (await cn.QueryAsync<TenderOpexDto>(sql, new { TenderId = tenderId })).AsList();
        }

        public async Task<TenderOpexDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            return await cn.QueryFirstOrDefaultAsync<TenderOpexDto>(
                "SELECT * FROM dbo.VW_TenderOpex WHERE Id=@Id", new { Id = id });
        }
    }
}
