using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;


namespace NetFlow.ReadModel.Tenders
{
    public sealed class TenderReadService
    {
        private readonly ReadModelOptions _opt;

        public TenderReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<PagedResult> ListAsync(int firmId, PagedRequest pagedRequest)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            const string sql = """
                SELECT COUNT(1)
                FROM dbo.VW_Tender WITH (NOLOCK)
                WHERE (@FirmId IS NULL OR FirmId = @FirmId);

                SELECT *
                FROM dbo.VW_Tender WITH (NOLOCK)
                WHERE (@FirmId IS NULL OR FirmId = @FirmId)
                ORDER BY Id DESC
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;
            """;

            using var multi = await cn.QueryMultipleAsync(sql, new
            {
                FirmId = firmId,
                Skip = pagedRequest.Skip,
                Take = pagedRequest.Take
            });

            var totalCount = await multi.ReadSingleAsync<int>();
            var items = (await multi.ReadAsync<TenderDto>()).AsList();

            return new PagedResult
            {
                data = items,
                totalCount = totalCount
            };
        }

        public async Task<TenderDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_Tender WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<TenderDto>(sql, new { Id = id });
        }
    }
}
