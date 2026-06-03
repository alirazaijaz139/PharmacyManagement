using System.Data;

namespace WindowsFormsApp1.Interfaces
{
    /// <summary>
    /// Generic Repository Interface - Abstraction
    /// Har CRUD repository ka contract define karta hai
    /// Agar database change ho toh sirf Repository change karo - Form ko haath nahi lagana
    /// </summary>
    public interface IRepository<T>
    {
        // Saara data laao

        DataTable GetAll();
        // Naya record add karo
        int Add(T entity);
        // Existing record update karo
        int Update(T entity);
        // Record delete karo ID se
        int Delete(int id);
    }
}