using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;


namespace NetFlow.ReadModel.Tenders
{
    public sealed class TenderReadService
    {
        private readonly ReadModelOptions _opt;

        public TenderReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<IReadOnlyList<TenderDto>> ListAsync(int? firmId = null)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = """
        SELECT *
        FROM dbo.VW_Tender WITH (NOLOCK)
        WHERE (@FirmId IS NULL OR FirmId = @FirmId)
        ORDER BY Id DESC
        """;

            var rows = await cn.QueryAsync<TenderDto>(sql, new { FirmId = firmId });
            return rows.AsList();
        }

        public async Task<TenderDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_Tender WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<TenderDto>(sql, new { Id = id });
        }
    }
}
