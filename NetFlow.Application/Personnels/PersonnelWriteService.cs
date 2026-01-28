using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Application.Roles;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace NetFlow.Application.Personnels
{
    public class PersonnelWriteService
    {
        private readonly INetFlowDbContext _db;
        public PersonnelWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreatePersonnelRequest request)
        {
            var personnel = new PersonnelEntity
            {
                AuthorityLevel = request.AuthorityLevel,
                CreatedAt = DateTime.UtcNow,
                CustomerCode = request.CustomerCode,
                Department = request.Department,
                Email = request.Email,
                DeletedAt = request.DeletedAt,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Salary = request.Salary,
                PersonnelCode = request.PersonnelCode,
                Title = request.Title,
                Phone = request.Phone,
                IsActive = request.IsActive,
                UserId = request.UserId
            };
            _db.Personnels.Add(personnel);
            await _db.SaveChangesAsync();
            return personnel.Id;
        }
        public async Task<int> EditAsync(EditPersonnelRequest request)
        {
            var personnel = await _db.Personnels.FirstAsync(x => x.Id == request.Id);
            personnel.AuthorityLevel = request.AuthorityLevel;
            personnel.CreatedAt = request.CreatedAt;
            personnel.CustomerCode = request.CustomerCode;
            personnel.Department = request.Department;
            personnel.Email = request.Email;
            personnel.DeletedAt = request.DeletedAt;
            personnel.FirstName = request.FirstName;
            personnel.LastName = request.LastName;
            personnel.Salary = request.Salary;
            personnel.PersonnelCode = request.PersonnelCode;
            personnel.Title = request.Title;
            personnel.Phone = request.Phone;
            personnel.IsActive = request.IsActive;
            personnel.UserId = request.UserId;
           // personnel.UpdatedAt = DateTime.UtcNow;
            _db.Personnels.Update(personnel);
            await _db.SaveChangesAsync();
            return personnel.Id;
        }
        public async Task<int> TerminateAsync(TerminatePersonnelRequest request)
        {
            var personnel = await _db.Personnels.FirstAsync(x => x.Id == request.Id);
            personnel.TerminationDate = request.TerminationDate;
            personnel.UpdatedAt = DateTime.UtcNow;
            _db.Personnels.Update(personnel);
            await _db.SaveChangesAsync();
            return personnel.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var personnel = await _db.Personnels.FirstOrDefaultAsync(x => x.Id == id);
            if (personnel != null)
            {
                _db.Personnels.Remove(personnel);
                await _db.SaveChangesAsync();
            }
        }
    }
}
