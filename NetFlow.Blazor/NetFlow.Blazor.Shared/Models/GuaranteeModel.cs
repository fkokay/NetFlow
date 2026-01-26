using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFlow.Blazor.Shared.Models
{
    public class GuaranteeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Firma seçimi zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen geçerli bir firma seçiniz.")]
        public int FirmId { get; set; }

        public string FirmName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Konu alanı boş bırakılamaz.")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Teminat Türü seçilmelidir.")]
        public string GuaranteeType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Teminat Şekli seçilmelidir.")]
        public string GuaranteeForm { get; set; } = string.Empty;

        [Required(ErrorMessage = "Belge Numarası girilmelidir.")]
        public string GuaranteeNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Düzenleyen Banka seçilmelidir.")]
        public string BankCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Döviz Tipi seçilmelidir.")]
        public string Currency { get; set; } = string.Empty;

        private decimal _guaranteeAmount;
        public decimal GuaranteeAmount
        {
            get => _guaranteeAmount;
            set { _guaranteeAmount = value; CalculateCommission(); }
        }

        private decimal _commissionRate;
        public decimal CommissionRate
        {
            get => _commissionRate;
            set { _commissionRate = value; CalculateCommission(); }
        }

        public decimal CommissionAmount { get; set; }

        public int CommissionPeriodId { get; set; }
        public string GuaranteeCommissionPeriodName { get; set; } = string.Empty;
        public DateTime GuaranteeDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string BankBranchCode { get; set; } = string.Empty;
        public string BankBranchName { get; set; } = string.Empty;

      
        public string? PublicAuthorityCode { get; set; }
        public string? PublicAuthorityName { get; set; }
        public string? ExpenseAccountCode { get; set; }
        public string? ExpenseAccountName { get; set; }

        public string TakasbankReferenceNo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public List<GuaranteeCommissionModel> GuaranteeCommissions { get; set; } = new List<GuaranteeCommissionModel>();

        private void CalculateCommission()
        {
            CommissionAmount = GuaranteeAmount * CommissionRate;
        }
    }
}
