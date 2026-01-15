using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.ReadModel.Guarantees;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Roles
{
    public class RoleReadService
    {
        private readonly ReadModelOptions _opt;
        public RoleReadService(ReadModelOptions opt) => _opt = opt;
        public async Task<IReadOnlyList<RoleDto>> ListAsync()
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var sql = "SELECT * FROM Role";
            return (await cn.QueryAsync<RoleDto>(sql)).AsList();
        }

    }
}
