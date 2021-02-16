using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BudgetYearConfiguration : IEntityTypeConfiguration<BudgetYear>
    {
        public void Configure(EntityTypeBuilder<BudgetYear> builder)
        {
           
        }
    }
}
