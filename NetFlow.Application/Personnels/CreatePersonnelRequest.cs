using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Application.Personnels
{
    public class CreatePersonnelRequest
    {
        public int FirmId { get; set; }
        public string? PersonnelCode { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }
        public decimal? Salary { get; set; }
        public string? Title { get; set; }
        public byte AuthorityLevel { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public int? UserId { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
