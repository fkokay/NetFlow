using Microsoft.AspNetCore.Components.Authorization;
using NetFlow.Blazor.Shared.Services;
using System.Net.Http.Headers;

namespace NetFlow.Blazor.Web.Security
{
    public sealed class ApiClientFactory : IApiClientFactory
    {
        private readonly AuthenticationStateProvider _auth;

        public ApiClientFactory(AuthenticationStateProvider auth)
        {
            _auth = auth;
        }

        public async Task<HttpClient> CreateAsync(HttpClient client)
        {
            var state = await _auth.GetAuthenticationStateAsync();
            var user = state.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var token = user.FindFirst("access_token")?.Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }
            }

            return client;
        }
    }
}
