using System.ComponentModel.DataAnnotations;

namespace NetFlow.Blazor.Shared.Models
{
    public class GuaranteeCommissionPeriodModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Dönem Adı boş bırakılamaz.")]
        public string PeriodName { get; set; } = string.Empty;
        public int Period { get; set; } 
    }
}
