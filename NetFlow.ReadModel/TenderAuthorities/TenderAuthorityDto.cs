using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.TenderAuthorities
{
    public sealed class TenderAuthorityDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }

        public string ParentAuthorityCode { get; set; }
        public string ParentAuthorityName { get; set; }

        public string UnitCode { get; set; }
        public string UnitName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
