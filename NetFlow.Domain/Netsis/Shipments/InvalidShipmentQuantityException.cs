using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Shipments
{
    public sealed class InvalidShipmentQuantityException : DomainException
    {
        public InvalidShipmentQuantityException()
            : base("INVALID_SHIPMENT_QTY", "Sevkiyat miktarı 0 veya negatif olamaz.") { }
    }
}
