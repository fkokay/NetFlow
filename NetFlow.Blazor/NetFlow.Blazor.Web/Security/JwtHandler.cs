using NetFlow.Blazor.Shared.Security;
using System.Net.Http.Headers;

namespace NetFlow.Blazor.Web.Security
{
    public sealed class JwtHandler : DelegatingHandler
    {
        private readonly ITokenStore _tokenStore;

        public JwtHandler(ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Ensure token is loaded (safe for prerender)
            await _tokenStore.LoadAsync();

            var token = _tokenStore.Token;

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
