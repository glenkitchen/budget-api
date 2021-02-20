using Application.Interfaces.Infrastructure;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
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

        //TODO       

        // Auditing (EntityFramework-Plus https://entityframework-plus.net/audit)
        // Query Cache (EntityFramework-Plus https://entityframework-plus.net/query-cache FromCacheAsync)       
        // Global Query Filters for Multi-Tenancy  

        // Dynamic paging https://entityframework-plus.net/ef-core-linq-dynamic

        //private readonly IUserService _user;

        //public BudgetDbContext(DbContextOptions<BudgetDbContext> options, IUserService user) : base(options)
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options)
        {
            //_user = user;
            //AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
            //{
            //    (context as BudgetDbContext).AuditEntries.AddRange(audit.Entries);
            //};
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply individual entity configurations                         
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            // Configure base entities          
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                                                         .Where(e => e.ClrType.IsSubclassOf(typeof(BaseEntity))))
            {
                modelBuilder.Entity(entityType.Name, b =>
                {
                    // Soft delete. Automatically exclude deletes in queries.
                    b.HasQueryFilter(CreateQueryFilterLambda(entityType.ClrType));
                    b.Property("CreatedDate").HasDefaultValueSql("getutcdate()");
                    b.Property("CreatedBy").HasMaxLength(256);
                    b.Property("LastModifiedBy").HasMaxLength(256);
                    b.Property("IsDisabled").HasDefaultValue(false);
                    b.Property("IsDeleted").HasDefaultValue(false);
                });
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                                                         .Where(e => e.ClrType.IsSubclassOf(typeof(NameEntity))))
            {
                modelBuilder.Entity(entityType.Name, b =>
                {
                    b.Property("Name").HasMaxLength(250).IsRequired();
                });
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                                                         .Where(e => e.ClrType.IsSubclassOf(typeof(CodeNameEntity))))
            {
                modelBuilder.Entity(entityType.Name, b =>
                {
                    // Soft delete. Include deleted in unique check.
                    b.HasIndex("Code", "IsDeleted").IsUnique().IsClustered(false);
                    b.Property("Code").HasMaxLength(50).IsRequired();
                    b.Property("Name").HasMaxLength(250).IsRequired();
                });
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                                                         .Where(e => e.ClrType.IsSubclassOf(typeof(OptionEntity))))
            {
                modelBuilder.Entity(entityType.Name, b =>
                {
                    b.ToTable($"opt_{entityType.ClrType.Name}");
                    // Soft delete. Include deleted in unique check.
                    b.HasIndex("Name", "IsDeleted").IsUnique().IsClustered(false);
                    b.Property("Name").HasMaxLength(250).IsRequired();
                });
            }

            //InvalidOperationException: Both 'Budget' and 'BaseEntity' are mapped to the table 'BaseEntity'.All the entity types in a hierarchy that don't have a discriminator must be mapped to different tables. 
            // See https://go.microsoft.com/fwlink/?linkid=2130430 for more information.            
            //builder.Entity<BaseEntity>().HasQueryFilter(e => !e.Disabled && !e.Deleted);

            /***********************************
            * Application Code
            ***********************************/
     
            // Configure many-to-many entities that have payload data
            modelBuilder.Entity<BudgetVersion>()
                   .HasMany(e => e.BudgetYears)
                   .WithMany(e => e.BudgetVersions)
                   .UsingEntity<BudgetVersionYear>(
                        e => e.HasOne<BudgetYear>().WithMany(),
                        e => e.HasOne<BudgetVersion>().WithMany());

            // Configure composite indexes
           modelBuilder.Entity<Budget>()
                       .HasIndex(e => new { e.BudgetVersionId, e.BudgetYearId, e.BudgetPeriodId, e.GlAccountId })
                       .IsUnique()
                       .IsClustered(false);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    // Note: Get current values for entity 
                    //entry.CurrentValues

                    case EntityState.Added:
                        // Use Sql instead. (HasDefaultValueSql("getutcdate()"))                        
                        // entry.Entity.CreatedDate = DateTime.UtcNow;
                        //entry.Entity.CreatedBy = _user.UserBy;                        
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        //entry.Entity.LastModifiedBy = _user.UserBy;
                        break;
                    // Soft delete. Intercept a delete and convert it to modified.
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        //entry.Entity.LastModifiedBy = _user.UserBy;
                        break;
                }
            }

            //var audit = new Audit();
            //audit.CreatedBy = "ZZZ Projects"; // Optional
            //ctx.SaveChanges(audit);

            return base.SaveChangesAsync(cancellationToken);
        }

        private LambdaExpression CreateQueryFilterLambda(Type type)
        {
            var parameter = Expression.Parameter(type, "e");

            var falseConstant = Expression.Constant(false);
            var propertyExpression = Expression.Property(parameter, "IsDeleted");
            var equalExpression = Expression.Equal(propertyExpression, falseConstant);

            var lambda = Expression.Lambda(equalExpression, parameter);

            return lambda;
        }

        // Audit Entities (EF Plus)
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }

        /***********************************
         * Application Code
         ***********************************/

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetPeriod> BudgetPeriods { get; set; }
        public DbSet<BudgetYear> BudgetYears { get; set; }
        public DbSet<GlAccount> GlAccounts { get; set; }

        // Join Entities
        public DbSet<BudgetVersionYear> BudgetVersionYears { get; set; }

        // Options 
        public DbSet<BudgetType> BudgetTypes { get; set; }
        public DbSet<BudgetVersion> BudgetVersions { get; set; }
        public DbSet<GlAccountType> GlAccountTypes { get; set; }
        public DbSet<SalesRegion> SalesRegions { get; set; }
    }
}
