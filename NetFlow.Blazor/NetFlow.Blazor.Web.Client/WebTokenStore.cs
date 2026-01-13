using Blazored.LocalStorage;
using NetFlow.Blazor.Shared;

namespace NetFlow.Blazor.Web.Client
{
    public class WebTokenStore : ITokenStore
    {
        private readonly ILocalStorageService _storage;
        public string? Token { get; private set; }

        public WebTokenStore(ILocalStorageService storage)
        {
            _storage = storage;
        }

        public async Task SetAsync(string token)
        {
            Token = token;
            await _storage.SetItemAsync("jwt", token);
        }

        public async Task LoadAsync()
        {
            Token = await _storage.GetItemAsync<string>("jwt");
        }

        public async Task ClearAsync()
        {
            Token = null;
            await _storage.RemoveItemAsync("jwt");
        }
    }
}
