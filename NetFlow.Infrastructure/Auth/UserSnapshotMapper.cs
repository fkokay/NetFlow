using NetFlow.Application.Auth;
using NetFlow.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Auth
{
    public static class UserSnapshotMapper
    {
        public static User ToDomain(UserSnapshot s)
        {
            var role = Role.Create(s.RoleCode, s.RoleName);
            var user = User.Create(s.Id,s.FullName, s.Email, s.FirmId, s.FirmCode,s.FirmName, role);

            foreach (var p in s.Permissions)
                user.Grant(p);

            return user;
        }
    }
}
