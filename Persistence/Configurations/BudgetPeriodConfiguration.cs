using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BudgetPeriodConfiguration : IEntityTypeConfiguration<BudgetPeriod>
    {
        public void Configure(EntityTypeBuilder<BudgetPeriod> builder)
        {
            
        }
    }
}
