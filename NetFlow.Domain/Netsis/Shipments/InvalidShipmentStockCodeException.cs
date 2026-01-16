using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Shipments
{
    public sealed class InvalidShipmentStockCodeException : DomainException
    {
        public InvalidShipmentStockCodeException()
            : base("INVALID_STOCK_CODE", "Stok kodu boş olamaz.") { }
    }
}
