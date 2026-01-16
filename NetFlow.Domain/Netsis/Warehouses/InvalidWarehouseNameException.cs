using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Netsis.Warehouses
{
    public sealed class InvalidWarehouseNameException : DomainException
    {
        public InvalidWarehouseNameException() : base("WAREHOUSE_INVALID_NAME", "Geçersiz depo adı")
        {
        }
    }
}
