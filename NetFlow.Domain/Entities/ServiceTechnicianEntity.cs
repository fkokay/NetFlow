using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("ServiceTechnician")]
    public class ServiceTechnicianEntity
    {
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        public int AssignedPersonnelId { get; set; }
        public int CreatedBy { get; set; }

        public DateTime? AssignedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
