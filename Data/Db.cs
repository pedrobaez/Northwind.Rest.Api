using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Assestment.Backend.Api.Data
{
    public static class Db
    {
        public static SqlConnection CreateConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
            }
            return new SqlConnection(connectionString);
        }
    }
}
