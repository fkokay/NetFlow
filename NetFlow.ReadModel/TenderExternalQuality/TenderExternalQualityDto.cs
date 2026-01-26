using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderExternalQuality
{
    public sealed class TenderExternalQualityDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int TenderAuthorityId { get; set; }

        public string ParentAuthorityCode { get; set; } = string.Empty;
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public string QualityCode { get; set; } = string.Empty;
        public string QualityName { get; set; } = string.Empty;

        public bool IsMandatory { get; set; }
        public bool IsCompliant { get; set; }
        public decimal Score { get; set; }
    }
}
