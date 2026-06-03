namespace WindowsFormsApp1.Validators
{
    /// <summary>
    /// Login form validation - Encapsulation
    /// Username aur password empty check karta hai
    /// </summary>
    public class LoginValidator
    {
        /// Returns: true = valid, false = invalid
        public bool IsValid(string username, string password)
        {
            // Username aur password dono bhare hone chahiye
            return !string.IsNullOrWhiteSpace(username) &&
                   !string.IsNullOrWhiteSpace(password);
        }
    }
}