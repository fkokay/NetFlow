using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.Utils;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.TenderDevices;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderDocuments
{
    public sealed class TenderRequiredDocumentReadService
    {
        private readonly ReadModelOptions _opt;
        public TenderRequiredDocumentReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<PagedResult> ListAsync(int tenderId, PagedRequest pagedRequest)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();

            parameters.Add("TenderId", tenderId);

            string whereSql = "WHERE TenderId = @TenderId";

            if (!string.IsNullOrEmpty(pagedRequest.filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.filter);
                whereSql += " AND " + sql;
                parameters.AddDynamicParams(p);
            }

            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(
                pagedRequest.sort,
                "Id DESC"
            );

            string countSql = $@"
                SELECT COUNT(1)
                FROM dbo.VW_TenderRequiredDocument WITH (NOLOCK)
                {whereSql}
            ";

            int totalCount = cn.ExecuteScalar<int>(
                countSql,
                parameters
            );

            if (pagedRequest.isCountQuery == true)
            {
                return new PagedResult
                {
                    data = Array.Empty<TenderRequiredDocumentDto>(),
                    totalCount = totalCount
                };
            }

            parameters.Add("@Skip", pagedRequest.skip ?? 0);
            parameters.Add("@Take", pagedRequest.take ?? 10);

            string dataSql = $@"
                SELECT *
                FROM dbo.VW_TenderRequiredDocument WITH (NOLOCK)
                {whereSql}
                ORDER BY {orderBy}
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";

            var data = cn.Query<TenderRequiredDocumentDto>(
                dataSql,
                parameters
            ).ToList();

            return new PagedResult
            {
                data = data,
                totalCount = totalCount
            };
        }

       
    }
}
