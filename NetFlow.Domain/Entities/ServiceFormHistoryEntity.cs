using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("ServiceFormHistory")]
    public class ServiceFormHistoryEntity
    {
        [Key]
        public int Id { get; set; }
        public int ServiceFormId { get; set; }
        public byte ActionType { get; set; }
        public byte? OldStatus { get; set; }
        public byte? NewStatus { get; set; }
        public int? OldPersonnelId { get; set; }
        public int? NewPersonnelId { get; set; }
        public string? Description { get; set; }
        public int ActionBy { get; set; }
        public DateTime ActionAt { get; set; }
        public string? IpAddress { get; set; }
        public string? Source { get; set; }
        public virtual ServiceFormEntity ServiceForm { get; set; } = null!;
        public virtual PersonnelEntity? OldPersonnel { get; set; }
        public virtual PersonnelEntity? NewPersonnel { get; set; }
        public virtual PersonnelEntity ActionByPersonnel { get; set; } = null!;
    }
}
