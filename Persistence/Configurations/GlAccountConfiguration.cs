using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class GlAccountConfiguration : IEntityTypeConfiguration<GlAccount>
    {
        public void Configure(EntityTypeBuilder<GlAccount> builder)
        {
            
        }
    }
}
