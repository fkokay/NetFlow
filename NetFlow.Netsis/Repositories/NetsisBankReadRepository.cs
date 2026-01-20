using Dapper;
using NetFlow.Application.Netsis.Banks;
using NetFlow.Application.Netsis.Products;
using NetFlow.Domain.Netsis.Banks;
using NetFlow.Domain.Netsis.Products;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisBankReadRepository : IBankReadRepository
    {
        private readonly ISqlProvider _sql;

        private readonly NetsisConnectionFactory _factory;
        public NetsisBankReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }
        public async Task<List<Bank>> GetBanks()
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Banks.sql");

            var dto = await con.QueryAsync<BankDto>(sql);

            return dto.Select(x =>
                      Bank.Create(
                          x.Banka_Kodu,
                          x.Banka_Adi
                      )).ToList();
        }
    }
}
