using NetFlow.Domain.Identity.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Domain.Identity
{
    public sealed class User
    {
        public UserId Id { get; }
        public string FullName { get; }
        public string Email { get; }
        public FirmId Firm { get; }
        public Role Role { get; }

        private readonly HashSet<string> _permissions = new();
        public IReadOnlyCollection<string> Permissions => _permissions;

        private User(UserId id,string fullName, string email, FirmId firm, Role role)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Firm = firm;
            Role = role;
        }

        public static User Create(int id,string fullName, string email, int firmId, string firmCode,string firmName, Role role)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidEmailException();

            return new User(new UserId(id),fullName, email, new FirmId(firmId, firmCode,firmName), role);
        }

        public void Grant(string permission)
            => _permissions.Add(permission.ToLowerInvariant());

        public bool HasPermission(string code)
            => Role.Has(code) || _permissions.Contains(code.ToLowerInvariant());
    }
}
