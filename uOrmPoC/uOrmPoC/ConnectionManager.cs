using Nzr.Orm.Core.Connection;
using System.Configuration;
using System.Data.SqlClient;

namespace uOrmPoC
{
    public class ConnectionManager : IConnectionManager
    {
        public SqlConnection Create(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public SqlConnection Create()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["Dummy"].ConnectionString);
        }

        public SqlTransaction CreateTransaction(SqlConnection sqlConnection)
        {
            return sqlConnection.BeginTransaction();
        }
    }
}
