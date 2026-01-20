using NetFlow.Application.Netsis.Banks;
using NetFlow.Domain.Netsis.BankBranches;
using NetFlow.Domain.Netsis.Banks;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.BankBranches
{
    public class BankBranchService
    {
        private readonly IBankBranchReadRepository _readRepo;

        public BankBranchService(IBankBranchReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<List<BankBranch>> GetBankBranches(string bankCode)
        {
            return await _readRepo.GetBankBranches(bankCode);
        }
    }
}
