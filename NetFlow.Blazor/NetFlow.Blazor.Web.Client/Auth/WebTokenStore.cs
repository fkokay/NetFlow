using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace NetFlow.Blazor.Web.Client.Auth
{

    public sealed class WebTokenStore : ITokenStore
    {
        private readonly IJSRuntime _js;
        private bool _isBrowser;

        public WebTokenStore(IJSRuntime js)
        {
            _js = js;
        }

        private async Task<bool> IsBrowserAsync()
        {
            if (_isBrowser) return true;

            try
            {
                await _js.InvokeVoidAsync("eval", "true");
                _isBrowser = true;
            }
            catch
            {
                return false; // SSR
            }

            return true;
        }

        public async Task<string?> GetAsync()
        {
            if (!await IsBrowserAsync())
                return null;

            return await _js.InvokeAsync<string>("localStorage.getItem", "jwt");
        }

        public async Task SetAsync(string token)
        {
            if (!await IsBrowserAsync())
                return;

            await _js.InvokeVoidAsync("localStorage.setItem", "jwt", token);
        }

        public async Task ClearAsync()
        {
            if (!await IsBrowserAsync())
                return;

            await _js.InvokeVoidAsync("localStorage.removeItem", "jwt");
        }
    }
}
