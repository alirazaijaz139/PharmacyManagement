using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1.Database
{
    

 public class ReportsRepository : BaseRepository
    {
        public decimal GetTotalSales()
        {
            string sql = "SELECT IFNULL(SUM(total),0) FROM sales";
            object result = ExecuteScalar(sql);
            return Convert.ToDecimal(result);
        }

        public decimal GetTotalPayments()
        {
            string sql = "SELECT IFNULL(SUM(amount),0) FROM payments WHERE status='Paid'";
            object result = ExecuteScalar(sql);
            return Convert.ToDecimal(result);
        }

        public decimal GetTodaySales()
        {
            string sql = "SELECT IFNULL(SUM(total),0) FROM sales WHERE DATE(sale_date)=CURDATE()";
            object result = ExecuteScalar(sql);
            return Convert.ToDecimal(result);
        }

        public string GetTopMedicine()
        {
            string sql = @"SELECT m.name 
                           FROM sale_items si
                           JOIN medicines m ON si.medicine_id = m.id
                           GROUP BY si.medicine_id
                           ORDER BY SUM(si.qty) DESC
                           LIMIT 1";
            object result = ExecuteScalar(sql);
            return result != null ? result.ToString() : "N/A";
        }
    }
}