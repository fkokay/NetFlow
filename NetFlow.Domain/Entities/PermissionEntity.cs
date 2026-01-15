using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetFlow.Domain.Entities
{
    [Table("Permission")]
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
    }
}
