using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.Utils;
using NetFlow.Domain.Common.Pagination;
using NetFlow.ReadModel.Tenders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Guarantees
{
    public sealed class GuaranteeReadService
    {
        private readonly ReadModelOptions _opt;
        public GuaranteeReadService(ReadModelOptions opt) => _opt = opt;


        public async Task<PagedResult> ListAsync(int firmId, PagedRequest pagedRequest,bool expiring = false,bool isActive = false,bool isRefunded = false)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("FirmId", firmId);

            string whereSql = "WHERE FirmId = @FirmId";
            if (expiring)
            {
                whereSql += " AND ExpiryDate BETWEEN GETDATE() AND DATEADD(MONTH,1,GETDATE())";
            }

            if (isActive)
            {
                whereSql += " AND GETDATE() <= ExpiryDate";
            }

            if (isRefunded)
            {
                whereSql += " AND IsRefunded = 1";
            }

            if (!string.IsNullOrEmpty(pagedRequest.filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.filter);
                whereSql += " AND " + sql;
                parameters.AddDynamicParams(p);
            }
            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.sort, "Id DESC");
            string countSql = $@"
                SELECT COUNT(1) FROM dbo.VW_Guarantee WITH (NOLOCK)
                {whereSql}
            ";

            string dataSql = $@"
                SELECT * FROM dbo.VW_Guarantee WITH (NOLOCK)
                {whereSql}
                ORDER BY {orderBy}
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";

            int totalCount = cn.ExecuteScalar<int>(
                countSql, parameters
            );

            if (pagedRequest.isCountQuery != null && pagedRequest.isCountQuery.HasValue)
            {
                return new PagedResult
                {
                    data = Array.Empty<GuaranteeDto>(),
                    totalCount = totalCount
                };
            }
            parameters.Add("@Skip", pagedRequest.skip ?? 0);
            parameters.Add("@Take", pagedRequest.take ?? 10);
            var data = cn.Query<GuaranteeDto>(dataSql, parameters).ToList();
            return new PagedResult
            {
                data = data,
                totalCount = totalCount
            };
        }
        public async Task<GuaranteeDto?> GetAsync(int id)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            var sql = "SELECT TOP 1 * FROM dbo.VW_Guarantee WITH (NOLOCK) WHERE Id=@Id";
            return await cn.QueryFirstOrDefaultAsync<GuaranteeDto>(sql, new { Id = id });
        }
    }
}
