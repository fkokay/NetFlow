using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("UserInRole")]
    public class UserInRoleEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; } = default!;

        public int RoleId { get; set; }
        public RoleEntity Role { get; set; } = default!;
    }
}
