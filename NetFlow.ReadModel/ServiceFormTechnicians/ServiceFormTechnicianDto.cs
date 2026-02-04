using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.ServiceFormTechnicians
{
    public class ServiceFormTechnicianDto
    {
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        public int AssignedPersonnelId { get; set; }
        public string? AssignedPersonnelCode { get; set; }
        public string? AssignedPersonnelName { get; set; }
        public string? AssignedPersonnelDepartment { get; set; }
        public string? AssignedPersonnelTitle { get; set; }
        public string? AssignedPersonnelEmail { get; set; }
        public string? AssignedPersonnelPhone { get; set; }
        public DateTime? AssignedAt { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedByPersonnelCode { get; set; }
        public string? CreatedByPersonnelName { get; set; }
        public string? CreatedByDepartment { get; set; }
        public string? CreatedByTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
