using NetFlow.Blazor.Shared.Auth;
using System.Security.Claims;

namespace NetFlow.Blazor.Web.Auth
{
    public sealed class ServerAppAuthState : IAppAuthState
    {
        public Task<ClaimsPrincipal> GetUserAsync()
        {
            // SSR → kullanıcı bilinmez
            return Task.FromResult(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public Task<bool> IsAuthenticatedAsync() =>
            Task.FromResult(false);
    }
}
