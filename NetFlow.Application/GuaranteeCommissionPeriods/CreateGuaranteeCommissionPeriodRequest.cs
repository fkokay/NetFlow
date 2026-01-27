using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.GuaranteeCommissionPeriods
{
    public class CreateGuaranteeCommissionPeriodRequest
    {
        public string PeriodName { get; set; } = string.Empty;
        public int Period { get; set; }
    }
}
