using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Application.Guarantees
{
    public class EditGuaranteeRequest
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public string FirmName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string GuaranteeType { get; set; } = string.Empty;
        public string GuaranteeForm { get; set; } = string.Empty;
        public string GuaranteeNumber { get; set; } = string.Empty;
        public decimal GuaranteeAmount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal CommissionRate { get; set; }
        [NotMapped]
        public decimal CommissionAmount { get; set; }
        public int CommissionPeriodId { get; set; }
        public string GuaranteeCommissionPeriodName { get; set; } = string.Empty;
        public DateTime GuaranteeDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string BankCode { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string BankBranchCode { get; set; } = string.Empty;
        public string BankBranchName { get; set; } = string.Empty;
        public string? PublicAuthorityCode { get; set; }
        public string? PublicAuthorityName { get; set; }
        public string? ExpenseAccountCode { get; set; }
        public string TakasbankReferenceNo { get; set; } = string.Empty;
        public bool IsRefunded { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
