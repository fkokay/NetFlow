using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderAuthorities
{
    public sealed class TenderAuthorityDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }

        public string ParentAuthorityCode { get; set; } = string.Empty;
        public string ParentAuthorityName { get; set; } = string.Empty;

        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
