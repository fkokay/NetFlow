using NetFlow.Blazor.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor
{
    public class MauiTokenStore : ITokenStore
    {
        public string? Token { get; private set; }

        public async Task SetAsync(string token)
        {
            Token = token;
            await SecureStorage.SetAsync("jwt", token);
        }

        public async Task LoadAsync()
        {
            Token = await SecureStorage.GetAsync("jwt");
        }

        public Task ClearAsync()
        {
            Token = null;
            SecureStorage.Remove("jwt");
            return Task.CompletedTask;
        }
    }
}
