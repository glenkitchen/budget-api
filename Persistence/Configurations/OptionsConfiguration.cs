using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BudgetTypeConfiguration : IEntityTypeConfiguration<BudgetType>
    {
        public void Configure(EntityTypeBuilder<BudgetType> builder)
        {
            
        }
    }
    public class BudgetVersionConfiguration : IEntityTypeConfiguration<BudgetVersion>
    {
        public void Configure(EntityTypeBuilder<BudgetVersion> builder)
        {
            
        }
    }
    public class GlAccountTypeConfiguration : IEntityTypeConfiguration<GlAccount>
    {
        public void Configure(EntityTypeBuilder<GlAccount> builder)
        {
            
        }
    }
    public class SalesRegionConfiguration : IEntityTypeConfiguration<SalesRegion>
    {
        public void Configure(EntityTypeBuilder<SalesRegion> builder)
        {
            
        }
    }
}
