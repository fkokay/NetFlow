using NetFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Application.ServiceForms
{
    public class CreateServiceFormRequest
    {
        public string ServiceFormNo { get; set; } = null!;
        public ServiceType ServiceType { get; set; }
        public ServiceStatus ServiceStatus { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string ProblemDescription { get; set; } = null!;
        public string? ServiceDescription { get; set; }
        public int? AssignedPersonnelId { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public bool IsOnSite { get; set; }
        public bool IsWarranty { get; set; }
        public decimal? LaborCost { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal TotalCost { get; set; }
        public string? Notes { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
