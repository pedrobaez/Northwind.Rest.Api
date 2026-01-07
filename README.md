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
