using Microsoft.Data.SqlClient;

namespace WebAPIRevision.Data
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
