using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Interfaces
{
    /// <summary>
    /// User Repository Interface - Abstraction
    /// Login aur password reset ka contract define karta hai
    /// </summary>
    public interface IUserRepository
    {
        // Username aur password se user dhundo - Login ke liye
        User GetByUsernamePassword(string username, string password);
        // Password reset karo - Forgot password ke liye
        int ResetPassword(string username, string newPassword);
        // Check karo username exist karta hai ya nahi
        bool UsernameExists(string username);
    }
}