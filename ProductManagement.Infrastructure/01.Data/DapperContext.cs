using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProductManagement.Infrastructure.Data
{
    public class DapperContext : IDatabaseContext
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;


        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IDbConnection> CreateOpenConnectionAsync()
        {
            var connection = CreateConnection();
            connection.Open();
            return await Task.FromResult( connection);
        }
    }
}
