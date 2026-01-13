using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderReaktif
{
    public sealed class TenderReaktifReadService
    {
        private readonly ReadModelOptions _opt;
        public TenderReaktifReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<IReadOnlyList<TenderReaktifDto>> ListAsync(int tenderId)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = """
        SELECT *
        FROM dbo.VW_TenderReaktif WITH(NOLOCK)
        WHERE TenderId = @TenderId
        ORDER BY UnitName, TestName
        """;

            return (await cn.QueryAsync<TenderReaktifDto>(sql, new { TenderId = tenderId })).AsList();
        }

        public async Task<TenderReaktifDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            return await cn.QueryFirstOrDefaultAsync<TenderReaktifDto>(
                "SELECT * FROM dbo.VW_TenderReaktif WHERE Id=@Id", new { Id = id });
        }
    }
}
