using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Services
{
    public class ClipboardManager
    {
        readonly ModuleLoader _moduleLoader;
        public ClipboardManager(ModuleLoader moduleLoader)
        {
            _moduleLoader = moduleLoader;
        }

        public async ValueTask CopyTextAsync(string text)
        {
            var module = await _moduleLoader.GetJSModuleSafeAsync("utils.js");
            if (module != null)
                await module.InvokeVoidAsync("copy", text);
        }
    }
}
