using MySql.Data.MySqlClient;
using System.Data;
using WindowsFormsApp1.Interfaces;
using WindowsFormsApp1.Models;


namespace WindowsFormsApp1.Database
{
    /// <summary>
    /// Customers table ke liye saari DB operations
    ///  Inheritance from BaseRepository
    /// Implements IRepository interface - Abstraction
    /// Polymorphism - GetTableName() override kiya hai
    /// </summary>
    public class CustomerRepository : BaseRepository, IRepository<Customer>
    {
        // Polymorphism - BaseRepository ka virtual method override kiya
        public override string GetTableName()
        {
            return "customers";
        }

        public override string GetSearchColumn()
        {
            return "name";
        }
        /// Saare customers database se laata hai
        public DataTable GetAll()
        {
            return ExecuteQuery("SELECT * FROM customers");
        }

        // ✅ Dropdown ke liye (id + name)
        public DataTable GetForDropdown()
        {
            return ExecuteQuery("SELECT id, name FROM customers");
        }

        // ✅  new Customer add karo
        public int Add(Customer c)
        {
            string sql = "INSERT INTO customers (name, account, phone, address, shop_name) " +
                         "VALUES (@name, @account, @phone, @address, @shop)";
            return ExecuteNonQuery(sql, new[]
            {
                new MySqlParameter("@name",    c.Name),
                new MySqlParameter("@account", c.Account),
                new MySqlParameter("@phone",   c.Phone),
                new MySqlParameter("@address", c.Address),
                new MySqlParameter("@shop",    c.ShopName)
            });
        }

        // ✅ Customer update karo
        public int Update(Customer c)
        {
            string sql = "UPDATE customers SET name=@name, account=@account, phone=@phone, " +
                         "address=@address, shop_name=@shop WHERE id=@id";
            return ExecuteNonQuery(sql, new[]
            {
                new MySqlParameter("@name",    c.Name),
                new MySqlParameter("@account", c.Account),
                new MySqlParameter("@phone",   c.Phone),
                new MySqlParameter("@address", c.Address),
                new MySqlParameter("@shop",    c.ShopName),
                new MySqlParameter("@id",      c.Id)
            });
        }

        // ✅ Customer delete karo
        public int Delete(int id)
        {
            return ExecuteNonQuery("DELETE FROM customers WHERE id=@id", new[]
            {
                new MySqlParameter("@id", id)
            });
        }
    }
}
