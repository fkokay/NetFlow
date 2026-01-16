using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Application.Users
{
    public class EditUserRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public bool Active { get; set; }
        public string? Password { get; set; }
        public string Roles { get; set; } = string.Empty;
        [NotMapped]
        public List<int>? RoleIds { get; set; } = new List<int>();
    }
}
