using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Roles;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NetFlow.Application.Users
{
    public class UserWriteService
    {
        private readonly INetFlowDbContext _db;
        public UserWriteService(INetFlowDbContext db)
        {
            _db = db;
        }
        public async Task<int> CreateAsync(CreateUserRequest request)
        {
            var user = new UserEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                Active = request.Active
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            var role = _db.Roles.FirstOrDefault(x => x.Name == "Kullanıcı");
            _db.UserInRoles.Add(new UserInRoleEntity
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            if (request.FirmIds != null && request.FirmIds.Any())
            {
                var userFirms = request.FirmIds.Select(firmId =>
                    new UserInFirmEntity
                    {
                        UserId = user.Id,
                        FirmId = firmId,
                        RoleId = role?.Id ?? 0 
                    }).ToList();

                _db.UserInFirms.AddRange(userFirms);
            }

            await _db.SaveChangesAsync();
            return user.Id;
        }


        public async Task<int> EditAsync(EditUserRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.Active = request.Active;

            if (!string.IsNullOrWhiteSpace(request.Password))
                user.Password = request.Password;

            var oldRoles = _db.UserInRoles.Where(x => x.UserId == user.Id);
            _db.UserInRoles.RemoveRange(oldRoles);

            if (request.RoleIds != null && request.RoleIds.Any())
            {
                foreach (var roleId in request.RoleIds)
                {
                    _db.UserInRoles.Add(new UserInRoleEntity
                    {
                        UserId = user.Id,
                        RoleId = roleId
                    });
                }
            }


            await _db.SaveChangesAsync();
            return user.Id;

        }


        public async Task DeleteAsync(int id)
        {
            
            var userRoles = await _db.UserInRoles
                .Where(x => x.UserId == id)
                .ToListAsync();

            if (userRoles.Any())
                _db.UserInRoles.RemoveRange(userRoles);
            var userFirms = await _db.UserInFirms
                .Where(x => x.UserId == id)
                .ToListAsync();
            if (userFirms.Any())
                _db.UserInFirms.RemoveRange(userFirms);
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
                _db.Users.Remove(user);

            await _db.SaveChangesAsync();
        }

    }
}
