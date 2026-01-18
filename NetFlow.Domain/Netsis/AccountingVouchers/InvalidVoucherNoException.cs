using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.AccountingVouchers
{
    public class InvalidVoucherNoException : DomainException
    {
        public InvalidVoucherNoException() : base("INVALID_VOUCHER_NO", "Muhasebe hesap kodu boş geçilemez")
        {
        }
    }
}
