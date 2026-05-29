using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Interfaces
{
    public interface IUserRepository
    {
        User GetByUsernamePassword(string username, string password);
        int ResetPassword(string username, string newPassword);
        bool UsernameExists(string username);
    }
}