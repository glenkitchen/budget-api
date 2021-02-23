# Persistence Project

## Debugging EF Core 5

* Before execution with Debug Views (`queryable`, `context.Model`, `context.ChangeTracker`) 
* After execution with `LogTo(Console.Writeline, LogLevel.Information)` and `EnableSensitiveDateLogging()`  

## Configuration


[Configure]((https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#manual-configuration)) the foreign key in a  one-to-many relationship, in the principal entity. E.g. 

    builder.HasOne(e => e.GlAccount)
           .WithMany(e => e.Budgets)
           .HasForeignKey(e => e.GlAccountId)
           .OnDelete(DeleteBehavior.ClientSetNull);

Configure a many-to-many relationship

TODO 

## Auditing

TODO 

## Soft delete 

Update an `IsDeleted` column instead of permanently removing a record.  

### Exclude deletes in queries

Automatically exclude deletes in queries with [global query filters](https://docs.microsoft.com/en-us/ef/core/querying/filters).  
Use a [Linq Lambda expression](https://docs.microsoft.com/en-us/dotnet/api/system.linq.expressions.expression.lambda?view=net-5.0) to apply the filter to a base entity.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                                                         .Where(e => e.ClrType.IsSubclassOf(typeof(BaseEntity))))
            {
                modelBuilder.Entity(entityType.Name, b =>
                {
                    b.HasQueryFilter(CreateQueryFilterLambda(entityType.ClrType));


    ...      
 
    private LambdaExpression CreateQueryFilterLambda(Type type)
    {
        var parameter = Expression.Parameter(type, "e");

        var falseConstant = Expression.Constant(false);
        var propertyExpression = Expression.Property(parameter, "IsDeleted");
        var equalExpression = Expression.Equal(propertyExpression, falseConstant);

        var lambda = Expression.Lambda(equalExpression, parameter);

        return lambda;
    }
  


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

Include the `IsDeleted` column in unique checks. E.g.

    modelBuilder.Entity(entityType.Name, b =>
    {
        b.HasIndex("Name", "IsDeleted").IsUnique().IsClustered(false);
    });