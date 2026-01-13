using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace NetFlow.Blazor.Shared.Auth
{
    public interface IAppAuthState
    {
        Task<ClaimsPrincipal> GetUserAsync();
        Task<bool> IsAuthenticatedAsync();
    }
}
