using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("Personnel")]
    public class PersonnelEntity
    {
        [Key]
        public int Id { get; set; }
        public string PersonnelCode { get; set; } = null!;
        public string CustomerCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; private set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Department { get; set; }
        public decimal? Salary { get; set; }
        public string? Title { get; set; }
        public byte AuthorityLevel { get; set; }
        public bool IsActive { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
