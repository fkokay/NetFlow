using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Services
{
    public interface IApiClientFactory
    {
        Task<HttpClient> CreateAsync(HttpClient client);
    }
}
