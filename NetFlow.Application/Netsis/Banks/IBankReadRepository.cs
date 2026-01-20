using NetFlow.Domain.Netsis.Banks;
using NetFlow.Domain.Netsis.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Netsis.Banks
{
    public interface IBankReadRepository
    {
        Task<List<Bank>> GetBanks();
    }
}
