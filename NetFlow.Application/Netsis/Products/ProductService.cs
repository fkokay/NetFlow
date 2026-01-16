using NetFlow.Application.Netsis.Shipments;
using NetFlow.Domain.Netsis.Products;
using NetFlow.Domain.Netsis.Shipments;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Products
{
    public class ProductService
    {
        private readonly IProductReadRepository _readRepo;

        public ProductService(IProductReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _readRepo.GetProducts();
        }
    }
}
