using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using UserCQRS.Domain.Interfaces.Repositories;

namespace UserCQRS.Infra.Data.Dapper.Connection
{
    public class DefaultSqlConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; set; }

        public DefaultSqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        
        public IDbConnection Connection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
