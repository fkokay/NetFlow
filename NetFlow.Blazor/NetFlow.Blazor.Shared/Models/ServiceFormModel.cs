using System.ComponentModel.DataAnnotations.Schema;

namespace NetFlow.Blazor.Shared.Models
{
    public class ServiceFormModel
    {
        public int Id { get; set; }
        public string ServiceFormNo { get; set; } = null!;
        public ServiceType ServiceType { get; set; }
        public ServiceStatus ServiceStatus { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string ProblemDescription { get; set; } = null!;
        public string? ServiceDescription { get; set; }
        public bool IsTechnicianAssigned { get; set; }
        public int? AssignedPersonnelId { get; set; }
        [NotMapped]
        public int? TechnicianId { get; set; }
        [NotMapped]
        public string? TechnicianName { get; set; }
        [NotMapped]
        public string? PersonnelCode { get; set; }
        [NotMapped]
        public string? AssignedPersonnelName { get; set; }
        [NotMapped]
        public string? AssignedPersonnelDepartment { get; set; }
        [NotMapped]
        public string? AssignedPersonnelTitle { get; set; }

        public DateTime? AssignedAt { get; set; } = DateTime.Now;
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }

        public bool IsOnSite { get; set; }
        public bool IsWarranty { get; set; }
        public string? TenderCode { get; set; }
        public string? TenderName { get; set; }
        public string? SubCustomerCode { get; set; }
        public string? SubCustomerName { get; set; }
        private decimal? _laborCost;
        public decimal? LaborCost
        {
            get => _laborCost;
            set
            {
                _laborCost = value;
                RecalculateTotalCost();
            }
        }

        private decimal? _materialCost;
        public decimal? MaterialCost
        {
            get => _materialCost;
            set
            {
                _materialCost = value;
                RecalculateTotalCost();
            }
        }
        [NotMapped]
        public decimal TotalCost { get; set; }

        public string? Notes { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        private void RecalculateTotalCost()
        {
            TotalCost = (_laborCost ?? 0) + (_materialCost ?? 0);
        }

    }

}
