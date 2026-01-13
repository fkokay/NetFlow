using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Guarantees
{
    public sealed class GuaranteeReadService
    {
        private readonly ReadModelOptions _opt;
        public GuaranteeReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<IReadOnlyList<GuaranteeDto>> ListAsync(int? firmId)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = """
        SELECT *
        FROM dbo.VW_Guarantee WITH(NOLOCK)
        WHERE (@FirmId IS NULL OR FirmId=@FirmId)
        ORDER BY Id DESC
        """;

            return (await cn.QueryAsync<GuaranteeDto>(sql, new { FirmId = firmId })).AsList();
        }

        public async Task<GuaranteeDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            return await cn.QueryFirstOrDefaultAsync<GuaranteeDto>(
                "SELECT * FROM dbo.VW_Guarantee WHERE Id=@Id", new { Id = id });
        }
    }
}
