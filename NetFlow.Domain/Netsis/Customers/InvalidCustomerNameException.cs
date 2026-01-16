using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Customers
{
    public class InvalidProductNameException : DomainException
    {
        public InvalidProductNameException() : base("CUSTOMER_INVALID_NAME", "Geçersiz müşteri adı")
        {
        }
    }
}
