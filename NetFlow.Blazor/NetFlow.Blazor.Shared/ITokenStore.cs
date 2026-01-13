using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared
{
    public interface ITokenStore
    {
        string? Token { get; }
        Task SetAsync(string token);
        Task LoadAsync();
        Task ClearAsync();
    }
}
