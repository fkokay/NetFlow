using Microsoft.AspNetCore.Components.Authorization;
using NetFlow.Blazor.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetFlow.Blazor.Web.Client
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenStore _tokenStore;

        public JwtAuthStateProvider(ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await _tokenStore.LoadAsync();

            if (string.IsNullOrWhiteSpace(_tokenStore.Token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(_tokenStore.Token);

            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void Notify()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
