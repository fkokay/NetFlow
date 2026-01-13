using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Common
{
    public interface ISqlProvider
    {
        string Get(string name);
    }
}
