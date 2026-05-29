using System.Data;

namespace WindowsFormsApp1.Interfaces
{
    public interface IRepository<T>
    {
        DataTable GetAll();
        int Add(T entity);
        int Update(T entity);
        int Delete(int id);
    }
}