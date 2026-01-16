using Microsoft.Data.SqlClient;
using NetFlow.Domain.Common;
using NetFlow.Domain.Identity;
using NetFlow.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Connection
{
    public sealed class NetsisConnectionFactory
    {
        private readonly NetFlowDbContext _db;
        private readonly ICurrentUser _currentUser;

        public NetsisConnectionFactory(NetFlowDbContext db, ICurrentUser currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public SqlConnection Create()
        {
            var firmId = _currentUser.User.Firm.Id;
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
