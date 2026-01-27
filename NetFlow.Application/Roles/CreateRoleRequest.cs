using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Roles
{
    public class CreateRoleRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
