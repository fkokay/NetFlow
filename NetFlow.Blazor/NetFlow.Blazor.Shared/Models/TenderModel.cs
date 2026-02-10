using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class TenderModel
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public int? TenderId { get; set; }
        public string? FirmName { get; set; }
        public string TenderCode { get; set; } = "";
        public string TenderName { get; set; } = "";
        public string PublicAuthorityCode { get; set; } = "";
        public string? PublicAuthorityName { get; set; }
        public string TenderType { get; set; } = "";
        public string TenderMethod { get; set; } = "";
        public DateTime TenderStartDate { get; set; }=DateTime.Now;
        public DateTime TenderEndDate { get; set; } = DateTime.Now.AddMonths(3);
        public int TenderDueDate { get; set; }
        public decimal TenderQuantity { get; set; }
        public decimal TenderAmount { get; set; }
        public string Currency { get; set; } = "TRY";
        public int? TemporaryGuaranteeRateId { get; set; }
        public string? TemporaryGuaranteeSubject { get; set; }
        public int? FinalGuaranteeRateId { get; set; }
        public string? FinalGuaranteeSubject { get; set; }
        public DateTime AnnouncementDate { get; set; } = DateTime.Now;
        public string? TenderStatus { get; set; } = "Devam Ediyor";
        public decimal? UnitPrice { get; set; }
        public DateTime? DocumentUploadDate { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ContractInvitationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [NotMapped]
        public List<TenderDeviceModel> Devices { get; set; } = new();
        [NotMapped]
        public List<TenderOpexModel> Opexs { get; set; } = new();
        [NotMapped]
        public List<TenderCapexModel> Capexs { get; set; } = new();
        [NotMapped]
        public List<TenderReaktifModel> Reaktifs { get; set; } = new();
        [NotMapped]
        public List<TenderRequiredDocument> Documents { get; set; } = new();
    }
}
