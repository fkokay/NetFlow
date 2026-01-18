using NetFlow.Blazor.Shared.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Security
{
    public interface ILoginService
    {
        Task LoginAsync(LoginRequest req);
        Task LogoutAsync();
    }
}
