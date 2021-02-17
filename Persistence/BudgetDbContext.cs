using Domain.Common;
using Domain.Entities;
//using Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Persistence
{
    public class BudgetDbContext : DbContext
    {
        /***********************************
         * Clean Architecture Template Code
         ***********************************/
        //Features 
        // Configuration
        // Logging (EnableSensitiveDataLogging with DbContextOptionsBuilder)
        // Disable Change Tracking for Queries (In BaseRepository )
        // Global Query Filters for Soft Delete 

        //TODO         
        // Auditing (EntityFramework-Plus https://entityframework-plus.net/audit)
        // Query Cache (EntityFramework-Plus https://entityframework-plus.net/query-cache FromCacheAsync)       
        // Global Query Filters for Multi-Tenancy  
        // UserService
        // Seed Data

        public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options)
        {
            //AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
            //{
            //    (context as BudgetDbContext).AuditEntries.AddRange(audit.Entries);
            //};
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply individual entity configurations                         
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configure base entities          
            foreach (var entityType in builder.Model.GetEntityTypes()
                                                    .Where(e => e.ClrType.IsSubclassOf(typeof(BaseEntity))))
            {
                builder.Entity(entityType.Name, b =>
                {
                    //b.HasAlternateKey("Guid");
                    b.Property("CreatedDate").HasDefaultValueSql("getutcdate()");
                    b.Property("CreatedBy").HasMaxLength(256);
                    b.Property("LastModifiedBy").HasMaxLength(256);
                    b.Property("Disabled").HasDefaultValue(false);
                    b.Property("Deleted").HasDefaultValue(false);
                });
            }

            foreach (var entityType in builder.Model.GetEntityTypes()
                                                    .Where(e => e.ClrType.IsSubclassOf(typeof(NameEntity))))
            {
                builder.Entity(entityType.Name, b =>
                {
                    b.Property("Name").HasMaxLength(250).IsRequired();
                });
            }

            foreach (var entityType in builder.Model.GetEntityTypes()
                                                    .Where(e => e.ClrType.IsSubclassOf(typeof(CodeNameEntity))))
            {
                builder.Entity(entityType.Name, b =>
                {
                    b.HasIndex("Code").IsUnique().IsClustered(false);
                    b.Property("Code").HasMaxLength(50).IsRequired();
                    b.Property("Name").HasMaxLength(250).IsRequired();
                });
            }

            foreach (var entityType in builder.Model.GetEntityTypes()
                                                    .Where(e => e.ClrType.IsSubclassOf(typeof(OptionEntity))))
            {
                builder.Entity(entityType.Name, b =>
                {
                    b.ToTable($"opt_{entityType.ClrType.Name}");
                    b.HasIndex("Name").IsUnique().IsClustered(false);
                    b.Property("Name").HasMaxLength(250).IsRequired();
                });
            }

            //InvalidOperationException: Both 'Budget' and 'BaseEntity' are mapped to the table 'BaseEntity'.All the entity types in a hierarchy that don't have a discriminator must be mapped to different tables. 
            // See https://go.microsoft.com/fwlink/?linkid=2130430 for more information.            
            //builder.Entity<BaseEntity>().HasQueryFilter(e => !e.Disabled && !e.Deleted);
            builder.Entity<Budget>().HasQueryFilter(e => !e.Disabled && !e.Deleted);
            builder.Entity<BudgetPeriod>().HasQueryFilter(e => !e.Disabled && !e.Deleted);
            //builder.Entity<BudgetYear>().HasQueryFilter(e => !e.Disabled && !e.Deleted);
            builder.Entity<GlAccount>().HasQueryFilter(e => !e.Disabled && !e.Deleted);
            builder.Entity<BudgetType>().HasQueryFilter(e => !e.Disabled && !e.Deleted);
            builder.Entity<BudgetVersion>().HasQueryFilter(e => !e.Disabled && !e.Deleted);
            builder.Entity<GlAccountType>().HasQueryFilter(e => !e.Disabled && !e.Deleted);
            builder.Entity<SalesRegion>().HasQueryFilter(e => !e.Disabled && !e.Deleted);

            /***********************************
            * Application Code
            ***********************************/

            // Configure many-to-many entities with payload data
            //builder.Entity<BudgetVersion>()
            //       .HasMany(e => e.BudgetYears)
            //       .WithMany(e => e.BudgetVersions)
            //       .UsingEntity<BudgetVersionYear>(
            //            e => e.HasOne<BudgetYear>().WithMany(),
            //            e => e.HasOne<BudgetVersion>().WithMany());

            // Configure composite indexes
            //builder.Entity<Budget>()
            //       .HasIndex(e => new { e.BudgetVersionId, e.BudgetYearId, e.BudgetPeriodId, e.GlAccountId })
            //       .IsUnique()
            //       .IsClustered(false);
                        
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }

            //var audit = new Audit();
            //audit.CreatedBy = "ZZZ Projects"; // Optional
            //ctx.SaveChanges(audit);

            return base.SaveChangesAsync(cancellationToken);
        }

        // Audit Entities (EF Plus)
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }

        /***********************************
         * Application Code
         ***********************************/

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetPeriod> BudgetPeriods { get; set; }
        //public DbSet<BudgetYear> BudgetYears { get; set; }
        public DbSet<GlAccount> GlAccounts { get; set; }

        // Join Entities
        //public DbSet<BudgetVersionYear> BudgetVersionYears { get; set; }

        // Options 
        public DbSet<BudgetType> BudgetTypes { get; set; }
        public DbSet<BudgetVersion> BudgetVersions { get; set; }
        public DbSet<GlAccountType> GlAccountTypes { get; set; }
        public DbSet<SalesRegion> SalesRegions { get; set; }
    }
}
