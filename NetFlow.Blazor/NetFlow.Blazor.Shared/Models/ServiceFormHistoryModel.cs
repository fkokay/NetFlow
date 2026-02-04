using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class ServiceFormHistoryModel
    {
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        public string ServiceFormNo { get; set; } = null!;
        public ServiceActionType ActionType { get; set; }
        public ServiceStatus? OldStatus { get; set; }
        public ServiceStatus? NewStatus { get; set; }
        public int? OldPersonnelId { get; set; }
        [NotMapped]
        public string? OldPersonnelCode { get; set; }
        [NotMapped]
        public string? OldPersonnelName { get; set; }
        public int? NewPersonnelId { get; set; }
        [NotMapped]
        public string? NewPersonnelCode { get; set; }
        [NotMapped]
        public string? NewPersonnelName { get; set; }
        public string? Description { get; set; }
        public int ActionBy { get; set; }
        [NotMapped]
        public string? ActionByPersonnelCode { get; set; }
        [NotMapped]
        public string? ActionByPersonnelName { get; set; }
        public DateTime ActionAt { get; set; }
        public string? IpAddress { get; set; }
        public string? Source { get; set; }
    }
}
