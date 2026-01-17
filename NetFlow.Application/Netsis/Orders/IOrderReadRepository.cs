using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Orders
{
    public interface IOrderReadRepository
    {
        Task<PagedResult> GetOrders(short orderType,PagedRequest request);
    }
}
