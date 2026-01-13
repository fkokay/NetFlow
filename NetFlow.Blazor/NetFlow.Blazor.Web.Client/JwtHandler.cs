using NetFlow.Blazor.Shared;
using System.Net.Http.Headers;

namespace NetFlow.Blazor.Web.Client
{
    public class JwtHandler : DelegatingHandler
    {
        private readonly IServiceProvider _provider;

        public JwtHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using var scope = _provider.CreateScope();
            var tokenStore = scope.ServiceProvider.GetRequiredService<ITokenStore>();

            await tokenStore.LoadAsync();

            if (!string.IsNullOrEmpty(tokenStore.Token))
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenStore.Token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
