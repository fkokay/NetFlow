using Microsoft.AspNetCore.Components.Authorization;
using NetFlow.Blazor.Shared.Security;
using System.Security.Claims;

namespace NetFlow.Blazor.Web.Security
{
    public sealed class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenStore _tokenStore;
        private static readonly AuthenticationState Anonymous =
            new(new ClaimsPrincipal(new ClaimsIdentity()));

        public JwtAuthStateProvider(ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await _tokenStore.LoadAsync();

            var token = _tokenStore.Token;
            if (string.IsNullOrWhiteSpace(token))
                return Anonymous;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                // Optional: Expiry check
                if (jwt.ValidTo < DateTime.UtcNow)
                {
                    await _tokenStore.ClearAsync();
                    return Anonymous;
                }

                var identity = new ClaimsIdentity(jwt.Claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch
            {
                // Corrupt token → logout
                await _tokenStore.ClearAsync();
                return Anonymous;
            }
        }

        public void NotifyUserAuthenticationStateChanged()
            => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
