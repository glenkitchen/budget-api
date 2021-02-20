# Domain Project

Clean architecture template code is in the **Common** folder.

Application code is in the **Entities** folder.

## Relationship Patterns

### One-to-many

By default, EF Core will create one-to-many relationships using [conventions](https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#conventions). 

In the dependent entity create: 
* A reference navigation property as `<principal entity name>` 
* An Id property as `<principal entity name>Id`  

If necessary, [manually configure]((https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#manual-configuration)) the foreign key with the principal entity. E.g. 

    builder.HasOne(x => x.GlAccount)
           .WithMany(s => s.Budgets)
           .HasForeignKey(x => x.GlAccountId)
           .OnDelete(DeleteBehavior.ClientSetNull);


### Many-to-many

From EF Core 5.0, many-to-many relationships can also be discovered by [convention](https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#other-relationship-patterns), *without* creating an explicit join entity. (EF creates the entity type internally.)

Just create a collection navigation property in both entities. 

However, if the many-to-many table contains additional data (known as payload data), then an explicit join entity must be created, and configured. E.g.

        
    builder.Entity<BudgetVersion>()
           .HasMany(e => e.BudgetYears)
           .WithMany(e => e.BudgetVersions)
           .UsingEntity<BudgetVersionYear>(
                          e => e.HasOne<BudgetYear>().WithMany(),
                          e => e.HasOne<BudgetVersion>().WithMany());   



  
## TODO 

Referential Action 

Immutable collection navigation property
    
    public ICollection<Post> Posts { get; } = new List<Post>();

Mapping with private constructor 

Idempotent POST with alternate keys