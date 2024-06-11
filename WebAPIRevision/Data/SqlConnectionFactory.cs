using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebAPIRevision.Data
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration configuration;
        public SqlConnectionFactory(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
       
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("dbcs"));
        }
    }
}
