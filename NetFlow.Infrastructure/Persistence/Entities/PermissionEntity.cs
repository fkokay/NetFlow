using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Infrastructure.Persistence.Entities
{
    [Table("Permission")]
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
    }
}
