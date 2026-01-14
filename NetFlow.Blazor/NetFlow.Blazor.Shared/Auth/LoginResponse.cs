using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }
}
