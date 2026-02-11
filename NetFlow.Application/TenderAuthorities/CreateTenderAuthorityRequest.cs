using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.TenderAuthorities
{
    public class CreateTenderAuthorityRequest
    {
        public int TenderId { get; set; }
        public string ParentAuthorityCode { get; set; } = null!;
        public string UnitCode { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
