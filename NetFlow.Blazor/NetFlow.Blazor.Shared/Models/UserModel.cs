using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetFlow.Blazor.Shared.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad zorunludur")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Soyad zorunludur")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mail zorunludur")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta formatı")]
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Şifre zorunludur")]
        public string Password { get; set; }
        public bool Active { get; set; } = true;
        public string Roles { get; set; } = string.Empty;
        public string Firms { get; set; } = string.Empty;
        public List<int>? RoleIds { get; set; } = new List<int>();
        public List<int>? FirmIds { get; set; } = new List<int>();
    }
}
