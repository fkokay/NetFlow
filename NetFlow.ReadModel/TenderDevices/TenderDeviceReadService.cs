using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderDevices
{
    public sealed class TenderDeviceReadService
    {
        private readonly ReadModelOptions _opt;
        public TenderDeviceReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<IReadOnlyList<TenderDeviceDto>> ListAsync(int tenderId)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = """
        SELECT *
        FROM dbo.VW_TenderDevice WITH(NOLOCK)
        WHERE TenderId = @TenderId
        ORDER BY CreatedAt DESC
        """;

            return (await cn.QueryAsync<TenderDeviceDto>(sql, new { TenderId = tenderId })).AsList();
        }

        public async Task<TenderDeviceDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            return await cn.QueryFirstOrDefaultAsync<TenderDeviceDto>(
                "SELECT * FROM dbo.VW_TenderDevice WHERE Id=@Id", new { Id = id });
        }
    }
}
