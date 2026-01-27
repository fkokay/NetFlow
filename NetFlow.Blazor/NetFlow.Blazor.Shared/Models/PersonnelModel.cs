using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class PersonnelModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Personel Kodu zorunludur.")]
        public string PersonnelCode { get; set; } = null!;
        [Required(ErrorMessage = "Cari Kodu zorunludur.")]
        public string CustomerCode { get; set; } = null!;
        [Required(ErrorMessage = "Cari Adı zorunludur.")]
        public string CustomerName { get; set; } = null!;
        [Required(ErrorMessage = "Ad seçimi zorunludur.")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Soyad seçimi zorunludur.")]
        public string LastName { get; set; } = null!;
        [NotMapped]
        public string? FullName { get; set; }
        [EmailAddress(ErrorMessage = "Geçersiz e-posta formatı")]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Department { get; set; }
        public string? Title { get; set; }
        public byte AuthorityLevel { get; set; }=1;
        public decimal? Salary { get; set; }
        public bool IsActive { get; set; } = true;
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
