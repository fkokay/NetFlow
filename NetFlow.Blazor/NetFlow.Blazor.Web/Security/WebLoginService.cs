using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using NetFlow.Blazor.Shared.Auth;
using NetFlow.Blazor.Shared.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static DevExpress.Data.Helpers.FindSearchRichParser;

namespace NetFlow.Blazor.Web.Security
{

    public sealed class WebLoginService : ILoginService
    {
        private readonly HttpClient _http;
        private CustomAuthStateProvider _authenticationStateProvider;

        public WebLoginService(HttpClient http, AuthenticationStateProvider authenticationStateProvider)
        {
            _http = http;
            _authenticationStateProvider = (CustomAuthStateProvider)authenticationStateProvider;
        }

        public async Task LoginAsync(LoginRequest req)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", req);

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (!response.IsSuccessStatusCode)
            {
                var message = result?.ErrorMessage
                              ?? "Giriş işlemi başarısız";

                throw new Exception(message);
            }

            if (result == null)
                throw new Exception("LoginResponse boş geldi");

            if (string.IsNullOrWhiteSpace(result.Token))
                throw new Exception(result.ErrorMessage ?? "Token boş geldi");

            await _authenticationStateProvider.UpdateAuthenticationState(result.Token);
        }

    }
}
