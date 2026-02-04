using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetFlow.Domain.Entities;

namespace NetFlow.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceFormTechnicianConfiguration : IEntityTypeConfiguration<ServiceFormTechnicianEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceFormTechnicianEntity> builder)
        {
            builder.ToTable("ServiceFormTechnician");

            builder.HasKey(x => x.Id);
        }
    }
}
