using Microsoft.Data.SqlClient;

namespace AppointmentScheduler
{
    public class Connection
    {
        public static SqlConnection conn = new();
        public static void Init(string connectionString)
        {
            conn.ConnectionString = connectionString;
            conn.Open();
        }
    }
}
