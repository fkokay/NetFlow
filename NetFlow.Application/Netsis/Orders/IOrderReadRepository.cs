using NetFlow.Domain.Netsis.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Orders
{
    public interface IOrderReadRepository
    {
        Task<List<Order>> GetOrders();
    }
}
