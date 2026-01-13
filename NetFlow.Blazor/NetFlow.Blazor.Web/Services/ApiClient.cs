using Microsoft.AspNetCore.Identity.Data;
using NetFlow.Blazor.Shared.Auth;
using System.Net.Http.Headers;

namespace NetFlow.Blazor.Web.Services
{
    public sealed class ApiClient
    {
        private readonly IHttpClientFactory _factory;
        private readonly IHttpContextAccessor _http;

        public ApiClient(IHttpClientFactory factory, IHttpContextAccessor http)
        {
            _factory = factory;
            _http = http;
        }

        private HttpClient Create()
        {
            var client = _factory.CreateClient("api");

            var token = _http.HttpContext?.Session.GetString("jwt");

            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        public async Task<LoginResponse> Login(LoginRequest req)
        {
            var client = Create();
            var res = await client.PostAsJsonAsync("api/auth/login", req);
            return await res.Content.ReadFromJsonAsync<LoginResponse>();
        }

    }
}
