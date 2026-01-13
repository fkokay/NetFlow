using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace NetFlow.Blazor.Web.Security
{
    public class JwtCookieAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _http;

        public JwtCookieAuthStateProvider(IHttpContextAccessor http)
        {
            _http = http;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = _http.HttpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity());
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
