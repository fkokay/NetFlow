using Dapper;
using NetFlow.Application.Netsis.Customers;
using NetFlow.Domain.Netsis.Customers;
using NetFlow.Domain.Netsis.Warehouses;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisCustomerReadRepository : ICustomerReadRepository
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory;

        public NetsisCustomerReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }
        public async Task<List<Customer>> GetCustomers()
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Customers.sql");

            var dto = await con.QueryAsync<CustomerDto>(sql);

            return dto.Select(x =>
                      Customer.Create(
                          x.SUBE_KODU,
                          x.ISLETME_KODU,
                          x.CARI_KOD,
                          x.CARI_ISIM
                      )).ToList();
        }
    }
}
