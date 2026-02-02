using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.ReadModel.ServiceFormDetails
{
    public class ServiceFormDetailDto
    {
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        [NotMapped]
        public string ServiceFormNo { get; set; } = null!;
        public int LineNo { get; set; }
        public ServiceDetailType DetailType { get; set; }
        public int? StockId { get; set; }
        public string? StockCode { get; set; }
        public string? StockName { get; set; }
        public string Description { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal TaxRate { get; set; }
        public decimal? LineAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool IsWarranty { get; set; }
        public bool IsBillable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
    }

}
