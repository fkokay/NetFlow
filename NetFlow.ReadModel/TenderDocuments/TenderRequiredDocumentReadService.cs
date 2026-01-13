using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderDocuments
{
    public sealed class TenderRequiredDocumentReadService
    {
        private readonly ReadModelOptions _opt;
        public TenderRequiredDocumentReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<IReadOnlyList<TenderRequiredDocumentDto>> ListAsync(int tenderId)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = """
        SELECT *
        FROM dbo.VW_TenderRequiredDocument WITH(NOLOCK)
        WHERE TenderId = @TenderId
        ORDER BY DocumentName
        """;

            return (await cn.QueryAsync<TenderRequiredDocumentDto>(sql, new { TenderId = tenderId })).AsList();
        }
    }
}
