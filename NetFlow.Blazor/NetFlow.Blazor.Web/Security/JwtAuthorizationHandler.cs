using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using NetFlow.Blazor.Shared.Security;
using System.Net.Http.Headers;

namespace NetFlow.Blazor.Web.Security
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly ITokenStore _tokenStore;

        public JwtAuthorizationHandler(ITokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = _tokenStore.GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
