For documentation on how to work with Entity Framework Core migrations, see:
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/

To create initial migration:
cd src/BookManagementSystem.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../BookManagementSystem.Api

To update database:
cd src/BookManagementSystem.Api
dotnet ef database update
