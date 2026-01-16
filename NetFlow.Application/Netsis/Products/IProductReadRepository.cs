using NetFlow.Domain.Netsis.Customers;
using NetFlow.Domain.Netsis.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Products
{
    public interface IProductReadRepository
    {
        Task<List<Product>> GetProducts();
    }
}
