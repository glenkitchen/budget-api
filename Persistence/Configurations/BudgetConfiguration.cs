using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations
{
    class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.HasIndex(e => new { e.BudgetVersionId, e.BudgetPeriodId, e.GlAccountId })
                .IsUnique()
                .IsClustered(false);

            //builder.HasOne(x => x.GLAccount)
            //    .WithMany(s => s.Budgets)
            //    .HasForeignKey(x => x.GLAccountId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(x => x.BudgetType)
            //    .WithMany(s => s.Budgets)
            //    .HasForeignKey(x => x.BudgetTypeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(x => x.BudgetYear)
            //    .WithMany(s => s.Budgets)
            //    .HasForeignKey(x => x.BudgetYearId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(x => x.BudgetVersion)
            //    .WithMany(s => s.Budgets)
            //    .HasForeignKey(x => x.BudgetVersionId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(x => x.BudgetPeriod)
            //    .WithMany(s => s.Budgets)
            //    .HasForeignKey(x => x.BudgetPeriodId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);           
        }
    }
}
