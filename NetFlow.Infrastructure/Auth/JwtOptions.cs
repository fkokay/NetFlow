using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Auth
{
    public sealed class JwtOptions
    {
        public string Issuer { get; set; } = "netflow";
        public string Audience { get; set; } = "netflow";
        public string SigningKey { get; set; } = default!; // config'den
        public int ExpMinutes { get; set; } = 60;
    }
}
