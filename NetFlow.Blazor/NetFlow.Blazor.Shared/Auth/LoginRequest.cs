using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirmCode { get; set; } = default!;

    }
}
