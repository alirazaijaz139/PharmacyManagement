namespace WindowsFormsApp1.Models
{
    /// <summary>
    /// User Model - Encapsulation
    /// Login user ka data store karta hai
    /// </summary>
    public class User
    {
        public int    Id       { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role     { get; set; } = "user"; // "admin" or "user"
    }
}
