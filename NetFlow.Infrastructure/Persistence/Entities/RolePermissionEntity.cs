using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.Entities
{
    [Table("RolePermission")]
    public class RolePermissionEntity
    {
        public int RoleId { get; set; }
        public RoleEntity Role { get; set; } = default!;

        public int PermissionId { get; set; }
        public PermissionEntity Permission { get; set; } = default!;
    }
}
