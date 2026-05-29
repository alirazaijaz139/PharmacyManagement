using MySql.Data.MySqlClient;
using System;
using System.Data;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Database
{
   



    public class PaymentRepository : BaseRepository
    {
        public DataTable GetSalesWithCustomers()
        {
            string sql = @"SELECT sales.id, customers.name 
                           FROM sales 
                           INNER JOIN customers ON sales.customer_id = customers.id";
            return ExecuteQuery(sql);
        }

        public DataTable GetAllPayments()
        {
            string sql = @"SELECT payments.id, customers.name, payments.amount, 
                           payments.status, payments.date
                           FROM payments
                           INNER JOIN sales ON payments.sale_id = sales.id
                           INNER JOIN customers ON sales.customer_id = customers.id";
            return ExecuteQuery(sql);
        }

        public bool PaymentExists(int saleId)
        {
            string sql = "SELECT COUNT(*) FROM payments WHERE sale_id=@id";
            object result = ExecuteScalar(sql, new[]
            {
                new MySqlParameter("@id", saleId)
            });
            return Convert.ToInt32(result) > 0;
        }

        public void AddPayment(int saleId, decimal amount, string status)
        {
            string sql = "INSERT INTO payments (sale_id, amount, status) VALUES (@s, @a, @st)";
            ExecuteNonQuery(sql, new[]
            {
                new MySqlParameter("@s", saleId),
                new MySqlParameter("@a", amount),
                new MySqlParameter("@st", status)
            });
        }

        public decimal GetSaleTotal(int saleId)
        {
            string sql = "SELECT total FROM sales WHERE id=@id";
            object result = ExecuteScalar(sql, new[]
            {
                new MySqlParameter("@id", saleId)
            });
            return result != null ? Convert.ToDecimal(result) : 0;
        }
    }
}
