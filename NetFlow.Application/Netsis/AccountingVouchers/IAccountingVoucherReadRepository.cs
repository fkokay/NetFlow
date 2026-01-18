using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.AccountingVouchers;
using NetFlow.Domain.Netsis.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.AccountingVouchers
{
    public interface IAccountingVoucherReadRepository
    {
        Task<PagedResult> GetAccountingVouchers(string accountCode, PagedRequest request);
    }
}
