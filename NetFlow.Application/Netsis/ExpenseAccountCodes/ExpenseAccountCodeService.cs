using NetFlow.Application.Netsis.Orders;
using NetFlow.Domain.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.ExpenseAccountCodes
{
    public class ExpenseAccountCodeService
    {
        private readonly IExpenseAccountCodeReadRepository _readRepo;

        public ExpenseAccountCodeService(IExpenseAccountCodeReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<PagedResult> GetExpenseAccountCodes(PagedRequest request)
        {
            return await _readRepo.GetExpenseAccountCodes(request);
        }
    }
}
