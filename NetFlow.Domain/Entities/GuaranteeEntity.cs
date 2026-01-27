using NetFlow.Domain.Tenders;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFlow.Domain.Entities
{
    [Table("Guarantee")]
    public class GuaranteeEntity
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string GuaranteeType { get; set; } = string.Empty;
        public string GuaranteeForm { get; set; } = string.Empty;
        public string GuaranteeNumber { get; set; } = string.Empty;
        public decimal GuaranteeAmount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal CommissionRate { get; set; }
        [NotMapped]
        public decimal? CommissionAmount { get; set; }
        public int CommissionPeriodId { get; set; }
        public DateTime GuaranteeDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string BankCode { get; set; } = string.Empty;
        public string BankBranchCode { get; set; } = string.Empty;
        public string? PublicAuthorityCode { get; set; }
        public string? ExpenseAccountCode { get; set; }
        public string TakasbankReferenceNo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public FirmEntity? Firm { get; set; }
        public GuaranteeCommissionPeriodEntity? CommissionPeriod { get; set; }
        public ICollection<TenderEntity> FinalGuaranteeTenders { get; set; } = [];
        public ICollection<TenderEntity> TemporaryGuaranteeTenders { get; set; } = [];
    }
}

