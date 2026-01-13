using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure.Persistence
{
    public class TenderFlowDbContext : DbContext
    {
        public TenderFlowDbContext(DbContextOptions<TenderFlowDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenderFlowDbContext).Assembly);
        }
    }
}
