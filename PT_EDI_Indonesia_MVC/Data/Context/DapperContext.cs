using System.Data;
using Microsoft.Data.SqlClient;

namespace PT_EDI_Indonesia_MVC.Data.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;

            // SWITCH BETWEEN SQL SERVER AND SQL SERVER EXPRESS CONNECTION STRING

            // _connectionString = _configuration.GetConnectionString("SqlConnection");
            _connectionString = _configuration.GetConnectionString("SQLExpress");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

    }
}