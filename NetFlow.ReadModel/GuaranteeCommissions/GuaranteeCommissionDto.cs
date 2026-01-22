using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.GuaranteeCommissions
{
    public class GuaranteeCommissionDto
    {
        public int Id { get; set; }
        public int GuaranteeId { get; set; }
        public DateTime CommissionStartDate { get; set; }
        public DateTime CommissionEndDate { get; set; }
        public decimal CommissionRate { get; set; }
        public decimal CommissionAmount { get; set; }
        public string Currency { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? BankReferenceNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string? Note { get; set; }
    }
}
