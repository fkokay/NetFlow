using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Text.Json;

namespace NetFlow.Blazor.Web.Security
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                await Task.Yield();

                Console.WriteLine("Test");
                // 1. Tarayıcı hafızasından token'ı oku
                var result = await _sessionStorage.GetAsync<string>("authToken");
                var token = result.Success ? result.Value : null;

                // 2. Token yoksa anonim (giriş yapmamış) kullanıcı dön
                if (string.IsNullOrWhiteSpace(token))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                // 3. Token varsa claim'leri parçala ve kimliği oluştur
                var claims = ParseClaimsFromJwt(token); // Önceki mesajdaki helper metodu

                var identity = new ClaimsIdentity(claims, "CustomAuth");
                var user = new ClaimsPrincipal(identity);

                Console.WriteLine($"Kullanıcı: {user.Identity.Name}, Yetkili mi: {user.Identity.IsAuthenticated}");
                return new AuthenticationState(user);
            }
            catch (Exception)
            {
                // 4. Prerendering sırasında veya hata anında anonim kullanıcı dön
                // Bu kısım "JS Interop" hatasını sessizce geçiştirmenizi sağlar
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task UpdateAuthenticationState(string? token)
        {
            ClaimsPrincipal claimsPrincipal;

            if (!string.IsNullOrWhiteSpace(token))
            {
                // 1. Token'ı tarayıcı hafızasına kaydet
                await _sessionStorage.SetAsync("authToken", token);

                // 2. Token içindeki verileri (claims) çözümle
                var claims = ParseClaimsFromJwt(token);
                var identity = new ClaimsIdentity(claims, "CustomAuth");
                claimsPrincipal = new ClaimsPrincipal(identity);
            }
            else
            {
                // 3. Logout durumu: Hafızayı temizle ve anonim kullanıcı ata
                await _sessionStorage.DeleteAsync("authToken");
                claimsPrincipal = _anonymous;
            }

            // 4. Tüm uygulamaya kimlik değişimini duyur
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            var claims = new List<Claim>();

            if (keyValuePairs != null)
            {
                foreach (var kvp in keyValuePairs)
                {
                    var value = kvp.Value.ToString();

                    // Eğer değer bir JSON dizisi ise (Örn: Birden fazla rol varsa)
                    if (kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in element.EnumerateArray())
                        {
                            claims.Add(new Claim(kvp.Key, item.ToString()));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(kvp.Key, value ?? ""));
                    }
                }
            }
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
