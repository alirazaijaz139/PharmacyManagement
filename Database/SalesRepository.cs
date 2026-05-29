using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Database
{
    /// <summary>
    /// Sales aur Sale_Items tables ke liye DB operations
    /// </summary>
    public class SalesRepository : BaseRepository
    {
        // ✅ Naya sale save karo (transaction ke saath)
        public long SaveSale(int customerId, decimal total, List<SaleItem> items)
        {
            using (var con = GetConnection())
            {
                con.Open();
                MySqlTransaction trans = con.BeginTransaction();

                try
                {
                    // 1. Sales table mein insert karo
                    MySqlCommand cmdSale = new MySqlCommand(
                        "INSERT INTO sales (customer_id, total) VALUES (@c, @t)", con, trans);
                    cmdSale.Parameters.AddWithValue("@c", customerId);
                    cmdSale.Parameters.AddWithValue("@t", total);
                    cmdSale.ExecuteNonQuery();

                    long saleId = cmdSale.LastInsertedId;

                    // 2. Har item insert karo
                    foreach (SaleItem item in items)
                    {
                        MySqlCommand cmdItem = new MySqlCommand(
                            "INSERT INTO sale_items (sale_id, medicine_id, price, qty, subtotal) " +
                            "VALUES (@s, @m, @p, @q, @sub)", con, trans);
                        cmdItem.Parameters.AddWithValue("@s",   saleId);
                        cmdItem.Parameters.AddWithValue("@m",   item.MedicineId);
                        cmdItem.Parameters.AddWithValue("@p",   item.Price);
                        cmdItem.Parameters.AddWithValue("@q",   item.Qty);
                        cmdItem.Parameters.AddWithValue("@sub", item.Subtotal);
                        cmdItem.ExecuteNonQuery();

                        // 3. Stock kam karo
                        MySqlCommand cmdStock = new MySqlCommand(
                            "UPDATE medicines SET qty = qty - @q WHERE id = @id", con, trans);
                        cmdStock.Parameters.AddWithValue("@q",  item.Qty);
                        cmdStock.Parameters.AddWithValue("@id", item.MedicineId);
                        cmdStock.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return saleId;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        // ✅ Sab sales laao
        public DataTable GetAll()
        {
            string sql = "SELECT s.id, c.name AS customer, s.total, s.sale_date " +
                         "FROM sales s JOIN customers c ON s.customer_id = c.id " +
                         "ORDER BY s.id DESC";
            return ExecuteQuery(sql);
        }
    }
}
