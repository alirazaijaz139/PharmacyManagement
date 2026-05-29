using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace WindowsFormsApp1.Database
{
    public class StockRepository : BaseRepository
    {
        public DataTable GetAll()
        {
            string sql = "SELECT id, name, company, qty FROM medicines";
            return ExecuteQuery(sql);
        }

        public void AddStock(int medicineId, int qty)
        {
            string sql = "UPDATE medicines SET qty = qty + @qty WHERE id = @id";
            ExecuteNonQuery(sql, new[]
            {
                new MySqlParameter("@qty", qty),
                new MySqlParameter("@id", medicineId)
            });
        }
    }
}
