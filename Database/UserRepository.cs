using System;
using MySql.Data.MySqlClient;
using System.Data;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Interfaces;

namespace WindowsFormsApp1.Database
{
    /// <summary>
    /// Users table ke liye DB operations 
    /// Inheritance from BaseRepository
    /// Implements IUserRepository interface - Abstraction
    /// Login aur password reset ke liye
    /// </summary>
    public class UserRepository : BaseRepository, IUserRepository
    {
        // ✅ Login check karta ha
        public User GetByUsernamePassword(string username, string password)
        {
            string sql = "SELECT * FROM login WHERE username=@u AND password=@p LIMIT 1";
            DataTable dt = ExecuteQuery(sql, new[]
            {
                new MySqlParameter("@u", username),
                new MySqlParameter("@p", password)
            });

            if (dt.Rows.Count == 0) return null;

            // Model mein data fill karo - Encapsulation
            DataRow row = dt.Rows[0];
            return new User
            {
                Id       = Convert.ToInt32(row["id"]),
                Username = row["username"].ToString(),
                Password = row["password"].ToString(),
                Role     = row["role"].ToString()
            };
        }

        // ✅ Password reset karo forgot password
        public int ResetPassword(string username, string newPassword)
        {
            string sql = "UPDATE login SET password=@p WHERE username=@u";
            return ExecuteNonQuery(sql, new[]
            {
                new MySqlParameter("@p", newPassword),
                new MySqlParameter("@u", username)
            });
        }

        // ✅ Check karo username exist karta hai ya nahi
        public bool UsernameExists(string username)
        {
            string sql = "SELECT COUNT(*) FROM login WHERE username=@u";
            object result = ExecuteScalar(sql, new[]
            {
                new MySqlParameter("@u", username)
            });
            return Convert.ToInt32(result) > 0;
        }
    }
}
