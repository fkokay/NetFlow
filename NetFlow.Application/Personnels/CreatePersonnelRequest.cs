using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Personnels
{
    public class CreatePersonnelRequest
    {
        public string CustomerCode { get; set; } = null!;
        public string PersonnelCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? FullName { get;  set; }
        public string? Email { get; set; }
        public decimal? Salary { get; set; }
        public string? Phone { get; set; }
        public string? Department { get; set; }
        public string? Title { get; set; }
        public byte AuthorityLevel { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
