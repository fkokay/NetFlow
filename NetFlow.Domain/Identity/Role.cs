using NetFlow.Domain.Common;
using NetFlow.Domain.Identity.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Identity
{
    public sealed class Role
    {
        public string Code { get; }
        public string Name { get; }

        private readonly HashSet<string> _permissions = new();
        public IReadOnlyCollection<string> Permissions => _permissions;

        private Role(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public static Role Create(string code, string name)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new InvalidEmailException();

            return new Role(code.ToUpperInvariant(), name);
        }

        public void Grant(string permission)
            => _permissions.Add(permission.ToLowerInvariant());

        public bool Has(string permission)
            => Code == "ADMIN" || _permissions.Contains(permission.ToLowerInvariant());
    }
}
