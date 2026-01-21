using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Customers;
using NetFlow.Domain.Netsis.Warehouses;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Customers
{
    public interface ICustomerReadRepository
    {
        Task<List<Customer>> GetCustomers();
        Task<PagedResult> GetCustomers(PagedRequest request);
    }
}
