using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Identity
{
    public sealed class CurrentUser : ICurrentUser
    {
        public User? User { get; set; }
        public bool IsAuthenticated => User != null;
    }
}
