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

        public string ParentAuthorityCode { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }

        public string QualityCode { get; set; }
        public string QualityName { get; set; }

        public bool IsMandatory { get; set; }
        public bool IsCompliant { get; set; }
        public decimal Score { get; set; }
    }
}
