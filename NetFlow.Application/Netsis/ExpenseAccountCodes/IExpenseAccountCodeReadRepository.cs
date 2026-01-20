using NetFlow.Domain.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.ExpenseAccountCodes
{
    public interface IExpenseAccountCodeReadRepository
    {
        Task<PagedResult> GetExpenseAccountCodes(PagedRequest request);
    }
}
