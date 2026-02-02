using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.ServiceReplacedParts
{
    public class ServiceReplacedPartDto
    {
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        public string ServiceFormNo { get; set; } = null!;
        public string? PartCode { get; set; }
        public string? PartName { get; set; }
        public int? Quantity { get; set; }
        public string? Currency { get; set; }
        public bool? IsWarranty { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
