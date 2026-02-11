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

        public async Task<int> CreateAsync(int firmId,CreatePersonnelRequest request)
        {
            var lastCode = await _db.Personnels
                .Where(x => x.FirmId == firmId && x.PersonnelCode.StartsWith("PRS"))
                .OrderByDescending(x => x.PersonnelCode)
                .Select(x => x.PersonnelCode)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastCode))
            {
                var numericPart = lastCode.Substring(3);
                if (int.TryParse(numericPart, out int parsed))
                    nextNumber = parsed + 1;
            }

            var newCode = $"PRS{nextNumber:D5}";

            var personnel = new PersonnelEntity
            {
                AuthorityLevel = request.AuthorityLevel,
                CreatedAt = DateTime.Now,
                CustomerCode = request.CustomerCode,
                FirmId= firmId,
                Email = request.Email,
                DeletedAt = request.DeletedAt,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DepartmentId=request.DepartmentId,
                Salary = request.Salary,
                PersonnelCode = newCode,
                Title = request.Title,
                Phone = request.Phone,
                IsActive = request.IsActive,
                HireDate = request.HireDate,
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
            personnel.DepartmentId = request.DepartmentId;
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
            personnel.UpdatedAt = DateTime.Now;
            _db.Personnels.Update(personnel);
            await _db.SaveChangesAsync();
            return personnel.Id;
        }
        public async Task<int> TerminateAsync(TerminatePersonnelRequest request)
        {
            var personnel = await _db.Personnels.FirstAsync(x => x.Id == request.Id);
            personnel.TerminationDate = request.TerminationDate;
            personnel.UpdatedAt = DateTime.UtcNow;
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


