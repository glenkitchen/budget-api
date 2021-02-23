# Persistence Project

## Debugging EF Core 5

* Before execution with Debug Views (`queryable`, `context.Model`, `context.ChangeTracker`) 
* After execution with `LogTo(Console.Writeline, LogLevel.Information)` and `EnableSensitiveDateLogging()`  

## Configuration


[Configure]((https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#manual-configuration)) foreign in the principal entity. E.g. 

    builder.HasOne(e => e.GlAccount)
           .WithMany(e => e.Budgets)
           .HasForeignKey(e => e.GlAccountId)
           .OnDelete(DeleteBehavior.ClientSetNull);

## Auditing

TODO 

## Soft delete 

Update an `IsDeleted` column instead of permanently removing a record.  

### Exclude deletes in queries

Automatically exclude deletes in queries with [global query filters](https://docs.microsoft.com/en-us/ef/core/querying/filters). 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                                                         .Where(e => e.ClrType.IsSubclassOf(typeof(BaseEntity))))
            {
                modelBuilder.Entity<BaseEntity>()
                            .HasQueryFilter(e => !e.IsDisabled && !e.IsDeleted);


### Convert deletes to updates

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:  
                    entry.State = EntityState.Modified;
                    entry.Entity.Deleted = true;

### Uniqueness

Include the delete column in unique checks.
