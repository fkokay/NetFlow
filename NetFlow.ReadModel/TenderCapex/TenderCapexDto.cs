using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderCapex
{
    public sealed class TenderCapexDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int? TenderAuthorityId { get; set; }

        public string ParentAuthorityCode { get; set; } = string.Empty;
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public string AssetCode { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
