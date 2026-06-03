using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using WindowsFormsApp1.Interfaces;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Database
{
    /// <summary>
    /// Medicines table ke liye saari DB operations
    /// Inheritance from BaseRepository
    /// Implements IRepository interface - Abstraction
    /// Polymorphism - GetTableName() override kiya hai
    /// </summary>
    public class MedicineRepository : BaseRepository, IRepository<Medicine>
    {
        // Polymorphism - BaseRepository ka virtual method override kiya
        public override string GetTableName()
        {
            return "medicines";
        }

        public override string GetSearchColumn()
        {
            return "name";
        }
        // ✅ Sab medicines laao database say
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM medicines";
            return ExecuteQuery(sql);
        }

        // ✅ Search by name ya company
        public DataTable Search(string keyword)
        {
            string sql = "SELECT * FROM medicines WHERE name LIKE @k OR company LIKE @k";
            return ExecuteQuery(sql, new[]
            {
                new MySqlParameter("@k", "%" + keyword + "%")
            });
        }

        // ✅  new Medicine add karo
        public int Add(Medicine m)
        {
            string sql = "INSERT INTO medicines (name, company, price, qty) VALUES (@name, @company, @price, @qty)";
            return ExecuteNonQuery(sql, new[]
            {
                new MySqlParameter("@name",    m.Name),
                new MySqlParameter("@company", m.Company),
                new MySqlParameter("@price",   m.Price),
                new MySqlParameter("@qty",     m.Qty)
            });
        }

        // ✅ Medicine update karo
        public int Update(Medicine m)
        {
            string sql = "UPDATE medicines SET name=@name, company=@company, price=@price, qty=@qty WHERE id=@id";
            return ExecuteNonQuery(sql, new[]
            {
                new MySqlParameter("@name",    m.Name),
                new MySqlParameter("@company", m.Company),
                new MySqlParameter("@price",   m.Price),
                new MySqlParameter("@qty",     m.Qty),
                new MySqlParameter("@id",      m.Id)
            });
        }

        // ✅ Medicine delete karo
        public int Delete(int id)
        {
            string sql = "DELETE FROM medicines WHERE id=@id";
            return ExecuteNonQuery(sql, new[]
            {
                new MySqlParameter("@id", id)
            });
        }

        // ✅ Stock kam karo (billing ke waqt)
        public int ReduceStock(int medicineId, int qty)
        {
            string sql = "UPDATE medicines SET qty = qty - @qty WHERE id = @id";
            return ExecuteNonQuery(sql, new[]
            {
                new MySqlParameter("@qty", qty),
                new MySqlParameter("@id",  medicineId)
            });
        }
    }
}
