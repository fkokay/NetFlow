using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("ServiceFormDetail")]
    public class ServiceFormDetailEntity
    {
        [Key]
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        public int LineNo { get; set; }
        public ServiceDetailType DetailType { get; set; }= ServiceDetailType.Undefined;
        public string? StockCode { get; set; }
        public string? StockName { get; set; }
        public string Description { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal TaxRate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal LineAmount { get; private set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal DiscountAmount { get; private set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TaxAmount { get; private set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalAmount { get; private set; }
        public bool IsWarranty { get; set; }
        public bool IsBillable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ServiceFormEntity ServiceForm { get; set; } = null!;
    }
}
