using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.Entities
{
    [Table("Role")]
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;

        public ICollection<RolePermissionEntity> RolePermissions { get; set; } = new List<RolePermissionEntity>();
        public ICollection<UserInFirmEntity> UserFirms { get; set; } = new List<UserInFirmEntity>();
        public ICollection<UserInRoleEntity> Users { get; set; } = new List<UserInRoleEntity>();
    }
}
