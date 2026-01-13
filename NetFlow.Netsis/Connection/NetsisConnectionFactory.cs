using Microsoft.Data.SqlClient;
using NetFlow.Domain.Common;
using NetFlow.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Connection
{
    public sealed class NetsisConnectionFactory
    {
        private readonly IdentityDbContext _db;
        private readonly ICurrentFirmProvider _firm;

        public NetsisConnectionFactory(IdentityDbContext db, ICurrentFirmProvider firm)
        {
            _db = db;
            _firm = firm;
        }

        public SqlConnection Create()
        {
            var firmId = _firm.GetFirmId();
            var firm = _db.Firms.Single(x => x.Id == firmId);

            var cs = new SqlConnectionStringBuilder
            {
                DataSource = firm.NetsisDbServer,
                InitialCatalog = firm.NetsisDbName,
                UserID = firm.NetsisDbUser,
                Password = firm.NetsisDbPassword,
                TrustServerCertificate = true,
                Encrypt = false,
                ApplicationName = "NetFlow.Netsis"
            }.ConnectionString;

            var con = new SqlConnection(cs);
            con.Open();
            return con;
        }
    }

}
