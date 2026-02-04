using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("ServiceReplacedPart")]
    public class ServiceReplacedPartEntity
    {
        [Key]
        public int Id { get; set; }
        public int ServiceRecordId { get; set; }
        public string? PartCode { get; set; }
        public string? PartName { get; set; }
        public int? Quantity { get; set; }
        public string? Currency { get; set; }
        public bool? IsWarranty { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
