using Blazored.LocalStorage;
using NetFlow.Blazor.Shared.Security;

namespace NetFlow.Blazor.Web.Security
{
    public class WebTokenStore : ITokenStore
    {
        private readonly ILocalStorageService _storage;
        private bool _loaded;

        public string? Token { get; private set; }

        public WebTokenStore(ILocalStorageService storage)
        {
            _storage = storage;
        }

        public async Task LoadAsync()
        {
            // Prevent JS calls during prerender
            if (_loaded)
                return;

            try
            {
                Token = await _storage.GetItemAsync<string>("jwt");
                _loaded = true;
            }
            catch (InvalidOperationException)
            {
                // JSRuntime not available yet (prerender)
                // Swallow and try again on next call
            }
        }

        public async Task SetAsync(string token)
        {
            Token = token;
            _loaded = true;

            await _storage.SetItemAsync("jwt", token);
        }

        public async Task ClearAsync()
        {
            Token = null;
            _loaded = true;

            await _storage.RemoveItemAsync("jwt");
        }
    }
}
