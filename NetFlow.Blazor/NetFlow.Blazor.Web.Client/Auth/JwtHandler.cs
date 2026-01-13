using System.Net.Http.Headers;

namespace NetFlow.Blazor.Web.Client.Auth
{
    public sealed class JwtHandler : DelegatingHandler
    {
        private readonly ITokenStore _store;
        public JwtHandler(ITokenStore store) => _store = store;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _store.GetAsync();

            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
