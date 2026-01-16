using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Role;
using NetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Modules
{
    public class ModuleWriteService
    {
        private readonly INetFlowDbContext _db;
        public ModuleWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateModuleRequest request)
        {
            var module = new ModuleEntity
            {
                Name = request.Name,
                Code = request.Code,
                IsActive = request.IsActive
            };
            _db.Modules.Add(module);
            await _db.SaveChangesAsync();
            return module.Id;
        }
        public async Task<int> EditAsync(EditModuleRequest request)
        {
            var module = await _db.Modules.FirstOrDefaultAsync(x => x.Id == request.Id);
            module.Code = request.Code;
            module.Name = request.Name;
            module.IsActive = request.IsActive;
            _db.Modules.Update(module);
            await _db.SaveChangesAsync();
            return module.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var module = await _db.Modules.FirstOrDefaultAsync(x => x.Id == id);
            if (module != null)
            {
                _db.Modules.Remove(module);
                await _db.SaveChangesAsync();
            }
        }
    }
}
