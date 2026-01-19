using NetFlow.Domain.Tenders;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFlow.Domain.Entities
{
    [Table("Guarantee")]
    public class GuaranteeEntity
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public string Subject { get; set; }
        public string GuaranteeType { get; set; }
        public string GuaranteeForm { get; set; }
        public string GuaranteeNumber { get; set; }
        public decimal GuaranteeAmount { get; set; }
        public string Currency { get; set; }
        public decimal CommissionRate { get; set; }
        [NotMapped]
        public decimal CommissionAmount { get; set; }
        public int CommissionPeriodId { get; set; }
        public DateTime GuaranteeDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string BankCode { get; set; }
        public string BankBranchCode { get; set; }
        public string PublicAuthorityCode { get; set; }
        public string TakasbankReferenceNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public FirmEntity Firm { get; set; }
        public GuaranteeCommissionPeriodEntity CommissionPeriod { get; set; }
        public ICollection<Tender> FinalGuaranteeTenders { get; set; }
        public ICollection<Tender> TemporaryGuaranteeTenders { get; set; }
    }
}

