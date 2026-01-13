using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace NetFlow.Blazor.Web.Auth
{
    public sealed class ServerAuthStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // SSR aşamasında herkes anonymous kabul edilir
            var identity = new ClaimsIdentity();
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
    }
}
