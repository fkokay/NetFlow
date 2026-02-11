using NetFlow.Domain.Enums;

namespace NetFlow.Application.ServiceDetails
{
    public class EditServiceDetailRequest
    {
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        public int LineNo { get; set; }
        public ServiceDetailType DetailType { get; set; }
        public string? StockCode { get; set; }
        public string? StockName { get; set; }
        public string Description { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string? Unit { get; set; } = "Adet";
        public decimal UnitPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal TaxRate { get; set; }
        public bool IsWarranty { get; set; }
        public bool IsBillable { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
