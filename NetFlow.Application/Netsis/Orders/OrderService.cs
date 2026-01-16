using NetFlow.Application.Netsis.Customers;
using NetFlow.Domain.Netsis.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Orders
{
    public class OrderService
    {
        private readonly ICustomerReadRepository _readRepo;

        public OrderService(ICustomerReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _readRepo.GetCustomers();
        }
    }
}
