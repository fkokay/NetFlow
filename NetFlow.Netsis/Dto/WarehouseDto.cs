using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Dto
{
    public class WarehouseDto
    {
        public short DEPO_KODU { get; set; }
        public string DEPO_ISMI { get; set; } = string.Empty;
        public short SUBE_KODU { get; set; }
    }
}
