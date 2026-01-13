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

        public string ParentAuthorityCode { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }

        public string AssetCode { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
