using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Services
{
    public class ModuleLoader : IAsyncDisposable
    {
        readonly CancellationTokenSource _disposeCts = new();
        readonly IJSRuntime _jsRuntime;
        readonly ConcurrentDictionary<string, ValueTask<IJSObjectReference?>> _modules = new();

        public ModuleLoader(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async ValueTask<IJSObjectReference?> GetJSModuleSafeAsync(string jsModule)
        {
            return await _modules.GetOrAdd(jsModule, async moduleName => {
                try
                {
                    return await _jsRuntime.InvokeAsync<IJSObjectReference>("import", _disposeCts.Token, $"/_content/NetFlow.Blazor.Shared/scripts/{jsModule}");
                }
                catch
                {
                    return null;
                }
            });
        }

        public async ValueTask DisposeAsync()
        {
            _disposeCts.Cancel();
            try
            {
                foreach (var item in _modules)
                {
                    var module = await item.Value;
                    if (module != null)
                        await module.DisposeAsync();
                }
                _modules.Clear();
            }
            catch (JSDisconnectedException) { }
            _disposeCts.Dispose();
        }
    }

}
