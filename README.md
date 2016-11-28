# PetaPoco Unit of Work and Repository patterns
C# Unit of Work and Repository patterns for the PetaPoco ORM. A great tool to share the PetaPoco database context across repositories and unit of work transactions.

## Installation
Run the `Create Tables.sql` file located in the `Data` folder

## Features
- Common Repository pattern CRUD methods
- Common Unit of Work pattern, sharing the database context across multiple repositories and queries
- Repository type to look up Model by another column/property, not just the identity column
- Repository type to look up Model by a foreign key
- Dynamic object queries: `repo.First(new { CustomerName = "Penelope Cruz", CustomerTypeId = 2 });`

```
// Wrap Unit of Work in disposable using block. A parameter of 'true' will tell it to use a transaction
using (var uow = new SampleUnitOfWork())
{
    // Create repository instance
    var repo = new ColumnLookupRepositoryBase<Customer>(uow);

    var customer = repo.First(new { CustomerName = "Penelope Cruz", CustomerTypeId = 2 });
    var customer2 = repo.FirstOrDefault(new { CustomerName = "Penelope Cruz", CustomerTypeId = 1 });

    var queryBuilderResult = repo.FindByColumns(new {CustomerName = "Penelope Cruz", CustomerTypeId = 2}).First();

    Assert.IsNotNull(customer);
    Assert.IsNull(customer2);
    Assert.IsNotNull(queryBuilderResult);
}
```

See unit tests for more usage examples