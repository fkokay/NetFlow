using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.GuaranteeCommissions
{
    public class CreateGuaranteeCommissionRequest
    {
        public int GuaranteeId { get; set; }
        public DateTime CommissionStartDate { get; set; }
        public DateTime CommissionEndDate { get; set; }
        public decimal CommissionRate { get; set; }
        public decimal CommissionAmount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public string? BankReferenceNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string? Note { get; set; }
    }
}
