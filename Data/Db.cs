using System.Data;
using System.Data.SqlClient;

namespace DEV_MANHA.Models.Data
{
    public class Db
    {
        public static string ConnectionString { get; set; }

        public static IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
