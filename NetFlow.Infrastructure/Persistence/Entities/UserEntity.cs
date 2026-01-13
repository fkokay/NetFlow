using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.Entities
{
    [Table("User")]
    public class UserEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public bool Active { get; set; }

        public ICollection<UserInFirmEntity> Firms { get; set; } = new List<UserInFirmEntity>();
        public ICollection<UserInRoleEntity> Roles { get; set; } = new List<UserInRoleEntity>();


    }
}
