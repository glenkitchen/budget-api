using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.HasOne(e => e.BudgetType)
                   .WithMany(e => e.Budgets)
                   .HasForeignKey(e => e.BudgetTypeId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne(e => e.BudgetVersion)
                   .WithMany(e => e.Budgets)
                   .HasForeignKey(e => e.BudgetVersionId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne(e => e.BudgetYear)
                   .WithMany(e => e.Budgets)
                   .HasForeignKey(e => e.BudgetYearId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne(e => e.BudgetPeriod)
                   .WithMany(e => e.Budgets)
                   .HasForeignKey(e => e.BudgetPeriodId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne(e => e.GlAccount)
                   .WithMany(e => e.Budgets)
                   .HasForeignKey(e => e.GlAccountId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
