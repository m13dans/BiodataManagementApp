using System.Data;
using BiodataManagement.Data.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BiodataManagement.Data.Context;

public class DbConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SQLConnection");
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);

}