using NetFlow.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Tenders
{
    public class Tender : AggregateRoot
    {
        public int FirmId { get; set; }
        public string FirmName { get; set; } = string.Empty;
        public string TenderCode { get; set; } = string.Empty;
        public string TenderName { get; set; } = string.Empty;
        public string PublicAuthorityCode { get; set; } = string.Empty;
        public string? PublicAuthorityName { get; set; }
        public string TenderType { get; set; } = string.Empty;
        public string TenderMethod { get; set; } = string.Empty;
        public DateTime TenderStartDate { get; set; }
        public DateTime TenderEndDate { get; set; }
        public int TenderDueDate { get; set; }
        public decimal TenderQuantity { get; set; }
        public decimal TenderAmount { get; set; }
        public string Currency { get; set; } = "TRY";
        public int? TemporaryGuaranteeRateId { get; set; }
        public string? TemporaryGuaranteeSubject { get; set; }
        public int? FinalGuaranteeRateId { get; set; }
        public string? FinalGuaranteeSubject { get; set; }
        public DateTime AnnouncementDate { get; set; }
        public DateTime? DocumentUploadDate { get; set; }
        public DateTime? ContractDate { get; set; }
        public string? TenderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
