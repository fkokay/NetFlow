using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Identity
{
    public sealed record UserRole(string Code)
    {
        public static UserRole Admin => new("ADMIN");
        public static UserRole Sales => new("SALES");
        public static UserRole Warehouse => new("WAREHOUSE");
    }
}
