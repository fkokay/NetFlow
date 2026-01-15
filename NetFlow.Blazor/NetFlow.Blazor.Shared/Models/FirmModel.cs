using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class FirmModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Firma Kodu zorunludur")]
        public string FirmCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Firma Adı zorunludur")]
        public string FirmName { get; set; } = string.Empty;
        public string? TaxNumber { get; set; } = string.Empty;
        public string? RegisterNumber { get; set; } = string.Empty;
        public string? NetsisRestApiUrl { get; set; } = string.Empty;
        public string? NetsisDbServer { get; set; } = string.Empty;
        public string? NetsisDbName { get; set; } = string.Empty;
        public string? NetsisDbUser { get; set; } = string.Empty;
        public string? NetsisDbPassword { get; set; } = string.Empty;
        public string? NetsisApplicationName { get; set; } = string.Empty;

        public string? NetsisUser { get; set; } = string.Empty;
        public string? NetsisPassword { get; set; } = string.Empty;
        public int NetsisCompanyCode { get; set; } = 1;
        public int NetsisBranchCode { get; set; } = 0;
        public string? EIRSSeri { get; set; } = string.Empty;
        public string? EFATSeri { get; set; } = string.Empty;
        public string? EARSSeri { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
        public ICollection<GuaranteeModel> Guarantees { get; set; } = new List<GuaranteeModel>();
    }
}
