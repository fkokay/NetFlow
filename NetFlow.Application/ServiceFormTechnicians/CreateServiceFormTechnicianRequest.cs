using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.ServiceFormTechnicians
{
    public class CreateServiceFormTechnicianRequest
    {
        public int ServiceFormId { get; set; }
        public int AssignedPersonnelId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
