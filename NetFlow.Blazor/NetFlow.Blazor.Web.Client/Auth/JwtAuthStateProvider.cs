using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetFlow.Blazor.Web.Client.Auth
{
    public sealed class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenStore _store;
        public JwtAuthStateProvider(ITokenStore store) => _store = store;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _store.GetAsync();

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task SetTokenAsync(string token)
        {
            await _store.SetAsync(token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync()
        {
            await _store.ClearAsync();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
