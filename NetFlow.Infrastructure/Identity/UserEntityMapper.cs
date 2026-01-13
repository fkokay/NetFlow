using NetFlow.Domain.Identity;
using NetFlow.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Identity
{
    public static class UserEntityMapper
    {
        public static User Map(UserEntity user, UserInFirmEntity uif)
        {
            var role = Role.Create(uif.Role.Code, uif.Role.Name);

            foreach (var rp in uif.Role.RolePermissions)
                role.Grant(rp.Permission.Code);

            var domain = User.Create(user.Id, user.FirstName + " " + user.LastName, user.Email, uif.Firm.Id, uif.Firm.FirmCode, role);

            return domain;
        }
    }
}
