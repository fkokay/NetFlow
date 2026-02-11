using NetFlow.Domain.Tenders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("TenderAuthority")]
    public class TenderAuthorityEntity
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public string ParentAuthorityCode { get; set; } = null!;
        public string UnitCode { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public TenderEntity? Tender { get; set; }
    }
}
