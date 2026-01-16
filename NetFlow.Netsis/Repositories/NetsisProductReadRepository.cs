using Dapper;
using NetFlow.Application.Netsis.Products;
using NetFlow.Domain.Netsis.Products;
using NetFlow.Domain.Netsis.Warehouses;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisProductReadRepository : IProductReadRepository
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory;

        public NetsisProductReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;
        }

        public async Task<List<Product>> GetProducts()
        {
            using var con = _factory.Create();

            var sql = _sql.Get("Products.sql");

            var dto = await con.QueryAsync<ProductDto>(sql);

            return dto.Select(x =>
                      Product.Create(
                          x.SUBE_KODU,
                          x.ISLETME_KODU,
                          x.STOK_KODU,
                          x.STOK_ADI
                      )).ToList();
        }
    }
}
