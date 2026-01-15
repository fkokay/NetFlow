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
    }
}
