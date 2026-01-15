using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Auth;
using NetFlow.Domain.Common;
using NetFlow.Domain.Firms;
using NetFlow.Domain.Identity;
using NetFlow.Domain.Identity.Exceptions;
using NetFlow.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NetFlow.Infrastructure.Identity
{
    public sealed class UserService : IUserService
    {
        private readonly NetFlowDbContext _context;
        public UserService(NetFlowDbContext context)
        {
            _context = context;
        }
        public async Task<User> Authenticate(string email, string password,string firmCode)
        {
            var user = await _context.Users
        .Include(u => u.Firms)
            .ThenInclude(f => f.Firm)
        .Include(u => u.Firms)
            .ThenInclude(f => f.Role)
                .ThenInclude(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
        .SingleOrDefaultAsync(u => u.Email == email && u.Password == password && u.Active);

            if (user == null)
                throw new InvalidLoginException("E-posta adresi veya şifre hatalı ya da kullanıcı aktif değil");

            var firm = user.Firms.FirstOrDefault(f => f.Firm.FirmCode == firmCode);

            if (firm == null)
                throw new InvalidLoginException("Kullanıcı seçilen firmaya tanımlı değil.");

            return UserEntityMapper.Map(user, firm);
        }

        public async Task<User> GetById(int userId, string firmCode)
        {
            var user = await _context.Users
       .Include(u => u.Firms)
           .ThenInclude(f => f.Firm)
       .Include(u => u.Firms)
           .ThenInclude(f => f.Role)
               .ThenInclude(r => r.RolePermissions)
                   .ThenInclude(rp => rp.Permission)
       .SingleOrDefaultAsync(u => u.Id == userId && u.Active);

            if (user == null)
                throw new InvalidLoginException();

            var firm = user.Firms.FirstOrDefault(f => f.Firm.FirmCode == firmCode);
            if (firm == null)
                throw new InvalidLoginException("User not assigned to this firm");

            return UserEntityMapper.Map(user, firm);
        }
    }
}
