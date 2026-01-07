# Assestment.Backend.Api

ASP.NET Core Web API using ADO.NET (Microsoft.Data.SqlClient) against the Northwind database.

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- SQL Server (LocalDB, Express, Developer, or any edition)
- Northwind sample database

### Install Northwind Database

The Northwind database is a sample database originally created by Microsoft. You have several options:

#### Option 1: Download from Microsoft (Recommended)
1. Download the Northwind installer script:
   - [Northwind SQL Script (GitHub)](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs)
   - Direct link: [instnwnd.sql](https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/databases/northwind-pubs/instnwnd.sql)

2. Run the script in SQL Server Management Studio (SSMS) or Azure Data Studio:
   ```sql
   -- Open the instnwnd.sql file and execute it
   -- This will create the Northwind database
   ```

3. Or use sqlcmd from command line:
   ```powershell
   sqlcmd -S . -E -i instnwnd.sql
   ```

#### Option 2: Alternative Script Repository
- [Northwind Database Scripts (northwind-SQLServer4)](https://github.com/jpwhite3/northwind-SQLServer4)
  ```powershell
  git clone https://github.com/jpwhite3/northwind-SQLServer4.git
  cd northwind-SQLServer4
  sqlcmd -S . -E -i northwind.sql
  ```

#### Option 3: Using Docker (SQL Server in Container)
```powershell
# Pull and run SQL Server
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

# Download and run Northwind script
Invoke-WebRequest -Uri "https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/databases/northwind-pubs/instnwnd.sql" -OutFile "instnwnd.sql"
sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -i instnwnd.sql
```

### Verify Installation
```sql
USE Northwind;
SELECT COUNT(*) FROM Products;  -- Should return 77
SELECT COUNT(*) FROM Customers; -- Should return 91
```

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
