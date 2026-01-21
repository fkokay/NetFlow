using NetFlow.Application.Netsis.Warehouses;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Customers;
using NetFlow.Domain.Netsis.Warehouses;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Customers
{
    public class CustomerService
    {
        private readonly ICustomerReadRepository _readRepo;

        public CustomerService(ICustomerReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _readRepo.GetCustomers();
        }

        public async Task<PagedResult> GetPagedAsync(PagedRequest request)
        {
            return await _readRepo.GetCustomers(request);
        }
    }
}
