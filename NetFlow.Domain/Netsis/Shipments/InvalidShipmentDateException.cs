using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Shipments
{
    public sealed class InvalidShipmentDateException : DomainException
    {
        public InvalidShipmentDateException()
        : base("INVALID_SHIPMENT_DATE_RANGE", "Başlangıç tarihi bitiş tarihinden büyük olamaz.") { }
    }
}
