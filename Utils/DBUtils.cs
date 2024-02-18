using Microsoft.Data.SqlClient;

namespace OperationsService.Utils;

public class DBUtils
    {
        private static readonly string DB_NAME = "UserManagement";
        private static readonly string DB_USER_NAME = "sa";
        private static readonly string DB_PASSWORD = "12345";

        public static SqlConnection GetConnection()
        {
            SqlConnection? conn = null;
            string connectionString = $"Data Source=localhost,1433;Initial Catalog={DB_NAME};User Id={DB_USER_NAME};Password={DB_PASSWORD};TrustServerCertificate=true;";
            
            conn = new SqlConnection(connectionString);
            return conn;
        }
    }

