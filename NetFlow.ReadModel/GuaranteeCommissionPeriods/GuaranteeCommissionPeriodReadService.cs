using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.ReadModel.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.GuaranteeCommissionPeriods
{
    public sealed class GuaranteeCommissionPeriodReadService
    {
        private readonly ReadModelOptions _opt;
        public GuaranteeCommissionPeriodReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<List<GuaranteeCommissionPeriodDto>> ListAsync()
        {
            using var cn = new SqlConnection(_opt.ConnectionString);

            const string sql = @"
                SELECT
                    Id,
                    PeriodName
                FROM GuaranteeCommissionPeriod WITH (NOLOCK)
                ORDER BY PeriodName
            ";

            var data = await cn.QueryAsync<GuaranteeCommissionPeriodDto>(sql);
            return data.ToList();
        }
    }
}
