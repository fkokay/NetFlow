using NetFlow.Application.Netsis.Banks;
using NetFlow.Domain.Netsis.Banks;
using NetFlow.Domain.Netsis.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Banks
{
    public class BankService
    {
        private readonly IBankReadRepository _readRepo;

        public BankService(IBankReadRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<List<Bank>> GetBanks()
        {
            return await _readRepo.GetBanks();
        }
    }
}
