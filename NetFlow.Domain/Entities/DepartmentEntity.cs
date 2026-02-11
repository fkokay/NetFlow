using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetFlow.Domain.Entities
{
    public class DepartmentEntity
    {
        [Key]
        public int Id { get; set; }
        public string DepartmentCode { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public int? ParentDepartmentId { get; set; }
        public DepartmentEntity? ParentDepartment { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<DepartmentEntity>? Children { get; set; }
    }
}
