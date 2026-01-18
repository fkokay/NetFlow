using NetFlow.Blazor.Shared.Auth;
using NetFlow.Blazor.Shared.Security;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace NetFlow.Blazor.Security
{
    public class MauiLoginService : ILoginService
    {
        private readonly HttpClient _http;
        private readonly ITokenStore _token;

        public MauiLoginService(HttpClient http, ITokenStore token)
        {
            _http = http;
            _token = token;
        }

        public async Task LoginAsync(LoginRequest req)
        {
            var res = await _http.PostAsJsonAsync("api/auth/login", req);

            var jwt = await res.Content.ReadFromJsonAsync<LoginResponse>();

            await _token.SetAsync(jwt!.Token);
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
