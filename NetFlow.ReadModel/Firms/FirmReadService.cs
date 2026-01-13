using Dapper;
using Microsoft.Data.SqlClient;
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

        public async Task<IReadOnlyList<FirmDto>> ListAsync()
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = """
        SELECT *
        FROM dbo.VW_Firm WITH(NOLOCK)
        ORDER BY Id DESC
        """;

            return (await cn.QueryAsync<FirmDto>(sql, new {  })).AsList();
        }

        public async Task<FirmDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            return await cn.QueryFirstOrDefaultAsync<FirmDto>(
                "SELECT * FROM dbo.VW_Firm WHERE Id=@Id", new { Id = id });
        }
    }
}
