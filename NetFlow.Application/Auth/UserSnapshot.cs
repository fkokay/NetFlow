using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Auth
{
    public sealed class UserSnapshot
    {
        public int Id { get; init; }
        public string FullName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public int FirmId { get; init; }
        public string FirmCode { get; init; } = default!;
        public string RoleCode { get; init; } = default!;
        public string RoleName { get; init; } = default!;
        public List<string> Permissions { get; init; } = new();
    }
}
