using MySql.Data.MySqlClient;
using System.Data;

namespace WindowsFormsApp1.Database
{
    /// <summary>
    /// Base class for all Repositories
    /// Sab repositories is class se inherit karengi
    /// </summary>
    public abstract class BaseRepository
    {
        public virtual string GetTableName()
        {
            return string.Empty;
        }

        public virtual string GetSearchColumn()
        {
            return string.Empty;
        }

        protected MySqlConnection GetConnection()
        {
            return DbConnection.GetConnection();
        }

        /// <summary>
        /// INSERT, UPDATE, DELETE ke liye
        /// Returns: affected rows ka count
        /// </summary>
        protected int ExecuteNonQuery(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Single value return karta hai (jaise COUNT, MAX, etc.)
        /// </summary>
        protected object ExecuteScalar(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// SELECT queries ke liye — DataTable return karta hai
        /// </summary>
        protected DataTable ExecuteQuery(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    DataTable dt = new DataTable();
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        da.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Last inserted row ka ID return karta hai
        /// </summary>
        protected long ExecuteInsertGetId(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                    return cmd.LastInsertedId;
                }
            }
        }
    }
}
