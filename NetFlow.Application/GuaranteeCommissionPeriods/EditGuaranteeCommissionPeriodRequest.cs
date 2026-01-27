using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetFlow.Application.GuaranteeCommissionPeriods
{
    public class EditGuaranteeCommissionPeriodRequest
    {
        public int Id { get; set; }
        public string PeriodName { get; set; } = string.Empty;
        public int Period { get; set; }
    }
}
