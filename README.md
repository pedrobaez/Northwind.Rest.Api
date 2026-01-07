# Assestment.Backend.Api

ASP.NET Core Web API using ADO.NET (Microsoft.Data.SqlClient) against the Northwind database.

## Configure

Update the connection string in `appsettings.json`:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

If SQL Server is remote or uses SQL auth, change it accordingly, for example:

```
Server=YOUR_SERVER;Database=Northwind;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;
```

## Run

```powershell
Push-Location "C:\Users\pedro\source\repos\Assestment.Backend.Api"
dotnet run
```

The API listens on the URLs printed in the console (by default https).

## Endpoints

- GET `/api/products` – list products
- GET `/api/products/{id}` – product by ID
- GET `/api/customers` – list customers
- GET `/api/customers/{id}` – customer by ID

OpenAPI is enabled in Development at `/openapi/v1.json` and Swagger UI can be added if needed.

## Assessment Tasks

This project serves as a base for a backend assessment. Complete the following tasks:

### Task 1: Add Service Layer
- Create a service layer to separate business logic from controllers
- Implement `IProductService` and `ProductService` classes
- Implement `ICustomerService` and `CustomerService` classes
- Inject services into controllers using dependency injection
- Move all data access logic from controllers to services

### Task 2: Refactor Data Access with Dapper
- Replace ADO.NET with Dapper ORM
- Install `Dapper` NuGet package
- Refactor all SQL queries to use Dapper's simplified API
- Maintain async/await patterns
- Update the `Db` helper class or create a repository pattern

### Task 3: Add Swagger Documentation
- Install Swashbuckle.AspNetCore package
- Configure Swagger/OpenAPI in `Program.cs`
- Add XML documentation comments to controllers and models
- Test all endpoints using Swagger UI
- Document request/response examples

### Task 4: Create Additional Controllers
Create at least 3 more controllers for Northwind entities:
- **OrdersController**: GET list, GET by ID, GET by customer ID
- **CategoriesController**: GET list, GET by ID, GET products by category
- **SuppliersController**: GET list, GET by ID
- Consider adding POST/PUT/DELETE operations for full CRUD

### Bonus Points
- Add proper error handling and logging
- Implement DTOs (Data Transfer Objects)
- Add pagination for list endpoints
- Include unit tests
- Add input validation using FluentValidation
