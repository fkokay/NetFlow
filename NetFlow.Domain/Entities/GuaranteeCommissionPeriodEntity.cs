using System.ComponentModel.DataAnnotations.Schema;

namespace NetFlow.Domain.Entities
{
    [Table("GuaranteeCommissionPeriod")]
    public class GuaranteeCommissionPeriodEntity
    {
        public int Id { get; set; }
        public string PeriodName { get; set; }
        public int Period { get; set; }

        public ICollection<GuaranteeEntity> Guarantees { get; set; }
    }

}
