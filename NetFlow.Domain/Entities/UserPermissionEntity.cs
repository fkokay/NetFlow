using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Entities
{
    public class UserPermissionEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; } = default!;

        public int PermissionId { get; set; }
        public PermissionEntity Permission { get; set; } = default!;
    }
}
