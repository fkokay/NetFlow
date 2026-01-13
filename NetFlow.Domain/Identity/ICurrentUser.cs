using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Identity
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        User? User { get; }
    }
}
