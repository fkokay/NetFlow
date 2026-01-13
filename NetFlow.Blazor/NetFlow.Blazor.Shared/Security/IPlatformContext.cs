using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Security
{
    public interface IPlatformContext
    {
        bool IsWeb { get; }
        bool IsMaui { get; }
    }
}
