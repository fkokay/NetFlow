using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Orders
{
    public class OrderService
    {
        private readonly IOrderReadRepository _readRepo;

        public OrderService(IOrderReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<PagedResult> GetOrders(short orderType,PagedRequest request)
        {
            return await _readRepo.GetOrders(orderType,request);
        }
    }
}
