using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class ServiceDetailModel
    {
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        [NotMapped]
        public string ServiceFormNo { get; set; } = null!;
        public int LineNo { get; set; }
        public ServiceDetailType DetailType { get; set; }
        public string? StockCode { get; set; }
        public string? StockName { get; set; }
        public string Description { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }= "Adet";
        public decimal UnitPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal TaxRate { get; set; }
        [NotMapped]
        public decimal LineAmount => Quantity * UnitPrice;
        [NotMapped]
        public decimal DiscountAmount => LineAmount * (DiscountRate / 100m);
        [NotMapped]
        public decimal NetAmount => LineAmount - DiscountAmount;
        [NotMapped]
        public decimal TaxAmount => NetAmount * (TaxRate / 100m);
        [NotMapped]
        public decimal TotalAmount => NetAmount + TaxAmount;
        public bool IsWarranty { get; set; }
        public bool IsBillable { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public void ApplyWarrantyRules()
        {
            if (IsWarranty)
            {
                UnitPrice = 0;
                DiscountRate = 0;
                TaxRate = 0;
                Quantity = 0;
                IsBillable = false;
            }
        }

    }

}
