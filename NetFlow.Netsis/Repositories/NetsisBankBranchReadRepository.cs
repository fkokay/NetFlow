using Dapper;
using NetFlow.Application.Netsis.BankBranches;
using NetFlow.Application.Netsis.Banks;
using NetFlow.Domain.Netsis.BankBranches;
using NetFlow.Domain.Netsis.Banks;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisBankBranchReadRepository : IBankBranchReadRepository
    {

        private readonly ISqlProvider _sql;

        private readonly NetsisConnectionFactory _factory;
        public NetsisBankBranchReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }
     

        public async Task<List<BankBranch>> GetBankBranches(string bankCode)
        {
            using var con = _factory.Create();

            var sql = _sql.Get("BankBranches.sql");

            var dto = await con.QueryAsync<BankBranchDto>(
                sql,
                new { BankCode = bankCode }
            );

            return dto.Select(x =>
                      BankBranch.Create(
                          x.Sube_Kodu,
                          x.Sube_Adi
                      )).ToList();
        }
    }
}
