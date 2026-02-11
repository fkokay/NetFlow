using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceTechnicianConfiguration : IEntityTypeConfiguration<ServiceTechnicianEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceTechnicianEntity> builder)
        {
            builder.ToTable("ServiceTechnician");

            builder.HasKey(x => x.Id);
        }
    }
}
