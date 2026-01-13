using NetFlow.Blazor.Shared.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetFlow.Blazor.Web.Client.Auth
{
    public sealed class JwtAppAuthState : IAppAuthState
    {
        private readonly ITokenStore _store;

        public JwtAppAuthState(ITokenStore store)
        {
            _store = store;
        }

        public async Task<ClaimsPrincipal> GetUserAsync()
        {
            var token = await _store.GetAsync();

            if (string.IsNullOrWhiteSpace(token))
                return new ClaimsPrincipal(new ClaimsIdentity());

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, "jwt"));
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await _store.GetAsync();
            return !string.IsNullOrWhiteSpace(token);
        }

        public async Task SetTokenAsync(string token)
        {
            await _store.SetAsync(token);
        }
    }
}
