using NetFlow.Blazor.Shared.Security;

namespace NetFlow.Blazor.Web.Security
{
    public class WebPlatformContext : IPlatformContext
    {
        public bool IsWeb => true;
        public bool IsMaui => false;
    }
}
