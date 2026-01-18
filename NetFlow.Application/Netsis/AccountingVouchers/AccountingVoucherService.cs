using NetFlow.Application.Netsis.Customers;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.AccountingVouchers
{
    public class AccountingVoucherService
    {
        private readonly IAccountingVoucherReadRepository _readRepo;

        public AccountingVoucherService(IAccountingVoucherReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<PagedResult> GetAccountingVouchers(string accountCode, PagedRequest request)
        {
            return await _readRepo.GetAccountingVouchers(accountCode,request);
        }
    }
}
