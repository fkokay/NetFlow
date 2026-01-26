using System.ComponentModel.DataAnnotations.Schema;

namespace NetFlow.Domain.Entities
{
    [Table("TenderPersonnel")]
    public class TenderPersonnelEntity
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int PersonnelId { get; set; }
    }
}

