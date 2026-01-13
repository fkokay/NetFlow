using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderExternalQuality
{
    public sealed class TenderExternalQualityReadService
    {
        private readonly ReadModelOptions _opt;
        public TenderExternalQualityReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<IReadOnlyList<TenderExternalQualityDto>> ListAsync(int tenderId)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = """
        SELECT *
        FROM dbo.VW_TenderExternalQuality WITH(NOLOCK)
        WHERE TenderId = @TenderId
        ORDER BY UnitName, QualityName
        """;

            return (await cn.QueryAsync<TenderExternalQualityDto>(sql, new { TenderId = tenderId })).AsList();
        }

        public async Task<TenderExternalQualityDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            return await cn.QueryFirstOrDefaultAsync<TenderExternalQualityDto>(
                "SELECT * FROM dbo.VW_TenderExternalQuality WHERE Id=@Id", new { Id = id });
        }
    }
}
