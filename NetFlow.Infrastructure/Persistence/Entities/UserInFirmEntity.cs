using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.Entities
{
    [Table("UserInFirm")]
    public sealed class UserInFirmEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; } = default!;

        public int FirmId { get; set; }
        public FirmEntity Firm { get; set; } = default!;

        // Bu firmadaki rolü
        public int RoleId { get; set; }
        public RoleEntity Role { get; set; } = default!;
    }
}
