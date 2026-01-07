using Assestment.Backend.Api.Data;
using Assestment.Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Assestment.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var results = new List<Product>();
            await using var conn = Db.CreateConnection(_configuration);
            await conn.OpenAsync();
            var sql = @"SELECT ProductID, ProductName, UnitPrice, UnitsInStock, Discontinued FROM Products";
            await using var cmd = new SqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                results.Add(new Product
                {
                    ProductID = reader.GetInt32(0),
                    ProductName = reader.GetString(1),
                    UnitPrice = reader.IsDBNull(2) ? null : reader.GetDecimal(2),
                    UnitsInStock = reader.IsDBNull(3) ? null : reader.GetFieldValue<short>(3),
                    Discontinued = reader.GetBoolean(4)
                });
            }
            return Ok(results);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            await using var conn = Db.CreateConnection(_configuration);
            await conn.OpenAsync();
            var sql = @"SELECT ProductID, ProductName, UnitPrice, UnitsInStock, Discontinued FROM Products WHERE ProductID = @id";
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var product = new Product
                {
                    ProductID = reader.GetInt32(0),
                    ProductName = reader.GetString(1),
                    UnitPrice = reader.IsDBNull(2) ? null : reader.GetDecimal(2),
                    UnitsInStock = reader.IsDBNull(3) ? null : reader.GetFieldValue<short>(3),
                    Discontinued = reader.GetBoolean(4)
                };
                return Ok(product);
            }
            return NotFound();
        }
    }
}
