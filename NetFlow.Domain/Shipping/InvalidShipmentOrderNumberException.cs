using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Shipping
{
    public sealed class InvalidShipmentOrderNumberException : DomainException
    {
        public InvalidShipmentOrderNumberException()
            : base("INVALID_ORDER_NO", "Sipariş numarası boş olamaz.") { }
    }
}
