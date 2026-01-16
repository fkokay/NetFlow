using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Firms;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Roles
{
    public sealed class RoleWriteService
    {
        private readonly INetFlowDbContext _db;
        public RoleWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateRoleRequest request)
        {
            var role = new RoleEntity
            {
                Code = request.Code,
                Name = request.Name
            };
            _db.Roles.Add(role);
            await _db.SaveChangesAsync();
            return role.Id;
        }
        public async Task<int> EditAsync(EditRoleRequest request)
        {
            var role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == request.Id);
            role.Code = request.Code;
            role.Name = request.Name;
            _db.Roles.Update(role);
            await _db.SaveChangesAsync();
            return role.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (role != null)
            {
                _db.Roles.Remove(role);
                await _db.SaveChangesAsync();
            }
        }
    }
}
