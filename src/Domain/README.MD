# Domain Project

Clean architecture template code is in the **Common** folder.

Application code is in the **Entities** folder.

## Relationship Patterns

### One-to-many

By default, EF Core will create one-to-many relationships using [conventions](https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#conventions). 

### Many-to-many

From EF Core 5.0, many-to-many relationships can also be discovered by [convention](https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#other-relationship-patterns), without creating an explicit join entity. (EF creates the entity type internally.)

However, if the many-to-many table contains additional data, then an explicit join entity must be created. 
  
## TODO? 

Idempotent POST with alternate keys