using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Products
{
    public class InvalidProductNameException : DomainException
    {
        public InvalidProductNameException() : base("Product_INVALID_NAME", "Geçersiz ürün adı")
        {
        }
    }
}
