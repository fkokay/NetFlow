using NetFlow.Domain.Netsis.BankBranches;
using NetFlow.Domain.Netsis.Banks;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.BankBranches
{
    public interface IBankBranchReadRepository
    {
        Task<List<BankBranch>> GetBankBranches(string bankCode);
    }
}
