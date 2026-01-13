using NetFlow.Blazor.Shared.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Security
{
    public class MauiPlatformContext : IPlatformContext
    {
        public bool IsWeb => false;
        public bool IsMaui => true;
    }
}
