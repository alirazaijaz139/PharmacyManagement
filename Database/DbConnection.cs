using MySql.Data.MySqlClient;

namespace WindowsFormsApp1.Database
{
    /// <summary>
    /// MySQL Database Connection Manager
    /// Pharmacy Management System (PMS)
    /// </summary>
    public static class DbConnection
    {
        // ✅ Apna MySQL password yahan update karo and server
        private static string _connectionString =
            "server=localhost;" +
            "user=root;" +
            "password=1234;" +
            "database=pharmacy;";

        public static string ConnectionString
        {
            get { return _connectionString; }
        }

        /// <summary>
        /// Naya MySQL connection return karta hai
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        /// <summary>
        /// Database connection test karta hai
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (MySqlConnection con = GetConnection())
                {
                    con.Open();
                    return con.State == System.Data.ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
