using Microsoft.EntityFrameworkCore;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Domain.Entities;
using NetFlow.Domain.Firms;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application.Firms
{
    public sealed class FirmWriteService
    {
        private readonly INetFlowDbContext _db;

        public FirmWriteService(INetFlowDbContext db)
        {
            _db = db;
        }

        public async Task<int> CreateAsync(CreateFirmRequest request)
        {
            var firm = new FirmEntity
            {
                FirmCode = request.FirmCode,
                FirmName = request.FirmName,
                TaxNumber = request.TaxNumber,
                RegisterNumber = request.RegisterNumber,
                NetsisRestApiUrl = request.NetsisRestApiUrl,
                NetsisDbServer=request.NetsisDbServer,
                NetsisDbName=request.NetsisDbName,
                NetsisDbUser=request.NetsisDbUser,
                NetsisDbPassword=request.NetsisDbPassword,
                NetsisApplicationName=request.NetsisApplicationName,
                NetsisUser=request.NetsisUser,
                NetsisPassword=request.NetsisPassword,
                NetsisCompanyCode=request.NetsisCompanyCode,
                NetsisBranchCode=request.NetsisBranchCode,
                EIRSSeri = request.EIRSSeri,
                EFATSeri = request.EFATSeri,
                EARSSeri = request.EARSSeri,
                CreatedAt = DateTime.UtcNow
            };

            _db.Firms.Add(firm);
            await _db.SaveChangesAsync();

            return firm.Id;
        }
        public async Task<int> EditAsync(EditFirmRequest request)
        {
            var firm = await _db.Firms.FirstAsync(x => x.Id == request.Id);
            firm.FirmCode = request.FirmCode;
            firm.FirmName = request.FirmName;
            firm.TaxNumber = request.TaxNumber;
            firm.RegisterNumber = request.RegisterNumber;
            firm.NetsisRestApiUrl = request.NetsisRestApiUrl;
            firm.NetsisDbServer = request.NetsisDbServer;
            firm.NetsisDbName = request.NetsisDbName;
            firm.NetsisDbUser = request.NetsisDbUser;
            firm.NetsisDbPassword = request.NetsisDbPassword;
            firm.NetsisApplicationName = request.NetsisApplicationName;
            firm.NetsisUser = request.NetsisUser;
            firm.NetsisPassword = request.NetsisPassword;
            firm.NetsisCompanyCode = request.NetsisCompanyCode;
            firm.NetsisBranchCode = request.NetsisBranchCode;
            firm.EIRSSeri = request.EIRSSeri;
            firm.EFATSeri = request.EFATSeri;
            firm.EARSSeri = request.EARSSeri;
            _db.Firms.Update(firm);
            await _db.SaveChangesAsync();
            return firm.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var firm = await _db.Firms.FirstOrDefaultAsync(x => x.Id == id);
            if (firm != null)
            {
                _db.Firms.Remove(firm);
                await _db.SaveChangesAsync();
            }
        }
    }
}
