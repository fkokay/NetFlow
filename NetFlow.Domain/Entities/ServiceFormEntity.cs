using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("ServiceForm")]
    public class ServiceFormEntity
    {
        [Key]
        public int Id { get; set; }
        public string ServiceFormNo { get; set; } = null!;
        public byte ServiceType { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public string ProblemDescription { get; set; } = null!;
        public string? ServiceDescription { get; set; }
        public byte ServiceStatus { get; set; }
        public int? AssignedPersonnelId { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public bool IsOnSite { get; set; }
        public bool IsWarranty { get; set; }
        public decimal? LaborCost { get; set; }
        public decimal? MaterialCost { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalCost { get; private set; }
        public string? Notes { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public virtual PersonnelEntity? AssignedPersonnel { get; set; }
    }
}
