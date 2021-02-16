using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options)
        {
        }

        public DbSet<BudgetYear> BudgetYears { get; set; }
        public DbSet<BudgetPeriod> BudgetPeriods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in builder.Model.GetEntityTypes()
                .Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity))))
            {
                builder.Entity(entityType.Name,
                    b =>
                    {
                        b.HasAlternateKey(new string[] { "Guid" });

                        // Default Values
                        b.Property("CreatedDate")
                         .HasDefaultValueSql("getutcdate()");

                        b.Property("Disabled")
                         .HasDefaultValue(false); //TODO .HasDefaultValueSql

                        b.Property("Deleted")
                         .HasDefaultValue(false); //TODO .HasDefaultValueSql

                        // Max Lengths
                        b.Property("CreatedBy")
                         .HasMaxLength(256);

                        b.Property("LastModifiedBy")
                         .HasMaxLength(256);
                    });
            }

            foreach (var entityType in builder.Model.GetEntityTypes()
                .Where(t => t.ClrType.IsSubclassOf(typeof(BaseNameEntity))))
            {
                builder.Entity(entityType.Name,
                    b =>
                    {
                        b.Property("Name")
                         .HasMaxLength(250);
                         //.IsRequired();
                    });
            }
            
            foreach (var entityType in builder.Model.GetEntityTypes()
                .Where(t => t.ClrType.IsSubclassOf(typeof(BaseCodeNameEntity))))
            {
                builder.Entity(entityType.Name,
                    b =>
                    {
                        b.Property("Code")
                         .HasMaxLength(50);
                         //.IsRequired();

                        b.HasIndex("Code")
                         .IsUnique()
                         .IsClustered(false);

                        b.Property("Name")
                         .HasMaxLength(250);
                         //.IsRequired();
                    });
            }
            
            foreach (var entityType in builder.Model.GetEntityTypes()
                .Where(t => t.ClrType.IsSubclassOf(typeof(BaseOption))))
            {
                builder.Entity(entityType.Name,
                    b =>
                    {
                        b.ToTable($"opt_{entityType.Name}");

                        // Max Lengths
                        b.Property("Name")
                         .HasMaxLength(250);
                         //.IsRequired();

                        b.HasIndex("Name")
                         .IsUnique()
                         .IsClustered(false);
                    });
            }

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)    
                {
                    //case EntityState.Added:
                    //    entry.Entity.CreatedDate = DateTime.UtcNow;
                    //    break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;                    
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
