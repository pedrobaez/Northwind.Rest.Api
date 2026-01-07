using Assestment.Backend.Api.Data;
using Assestment.Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Assestment.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CustomersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var results = new List<Customer>();
            await using var conn = Db.CreateConnection(_configuration);
            await conn.OpenAsync();
            var sql = @"SELECT CustomerID, CompanyName, ContactName, City, Country FROM Customers";
            await using var cmd = new SqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                results.Add(new Customer
                {
                    CustomerID = reader.GetString(0).Trim(),
                    CompanyName = reader.GetString(1),
                    ContactName = reader.IsDBNull(2) ? null : reader.GetString(2),
                    City = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Country = reader.IsDBNull(4) ? null : reader.GetString(4)
                });
            }
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(string id)
        {
            await using var conn = Db.CreateConnection(_configuration);
            await conn.OpenAsync();
            var sql = @"SELECT CustomerID, CompanyName, ContactName, City, Country FROM Customers WHERE CustomerID = @id";
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var customer = new Customer
                {
                    CustomerID = reader.GetString(0).Trim(),
                    CompanyName = reader.GetString(1),
                    ContactName = reader.IsDBNull(2) ? null : reader.GetString(2),
                    City = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Country = reader.IsDBNull(4) ? null : reader.GetString(4)
                };
                return Ok(customer);
            }
            return NotFound();
        }
    }
}
